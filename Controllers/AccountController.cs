using Application.DTOs;
using Application.Interfaces.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using pizzaServerApp.Migrations;
using pizzaServerApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ASPNetCoreApp.Controllers
{
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager; 
        private readonly ITokenServices _tokenServices;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly PizzaWebAppContext _context;

        public AccountController(PizzaWebAppContext context ,RoleManager<IdentityRole> roleManager, UserManager<User> userManager, ITokenServices tokenServices)
        {
            _userManager = userManager;
            _tokenServices = tokenServices;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpPost]
        [Route("api/account/login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Phone);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, "User"),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var token = _tokenServices.GenerateAccessToken(authClaims);
                var refreshToken = _tokenServices.GenerateRefreshToken();
                if (user.RefreshToken == null)
                {
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddYears(1);
                    //var info = new TokenInfo
                    //{
                    //    Usename = user.UserName,
                    //    RefreshToken = refreshToken,
                    //    RefreshTokenExpiry = DateTime.Now.AddDays(999)
                    //};
                    //_context.TokenInfo.Add(info);
                }

                else
                {
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddYears(1);
                }
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
                return Ok(
                        new newUserDTO
                        {
                            Phone = user.PhoneNumber,
                            Token = token,
                            RefreshToken = refreshToken
                        }
                    );

            }
            var errorMsg = new
            {
                message = "Вход не выполнен",
                error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
            };
            return Created("", errorMsg);
        }

        [HttpPost]
        [Route("api/account/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration([FromBody] RegisterDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDouble = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == model.Phone);
                    if (userDouble != null)
                    {
                        var errorMsg = new
                        {
                            message = "Пользователь с таким номером телефона уже существует!",
                            error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                        };
                        return Created("", errorMsg);
                    }

                    User user = new() { UserName = model.Phone, PhoneNumber = model.Phone };
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, "User"),

                    };
                    var token = _tokenServices.GenerateAccessToken(authClaims);
                    var refreshToken = _tokenServices.GenerateRefreshToken();
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync("User"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("User"));
                        }

                        var roleResult = await _userManager.AddToRoleAsync(user, "User");
                        if (!roleResult.Succeeded)
                        {
                            return Created("", "Не удалось добавить роль");
                        }
                        user.RefreshToken = refreshToken;
                        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddYears(1);
                        await _userManager.UpdateAsync(user);
                        return Ok(
                                new newUserDTO
                                {
                                    Phone = user.PhoneNumber,
                                    Token = token,
                                    RefreshToken = user.RefreshToken
                    }
                                );
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        var errorMsg = new
                        {
                            message = "Пользователь не добавлен",
                            error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                        };
                        return Created("", errorMsg);
                    }
                }
                else
                {
                    var errorMsg = new
                    {
                        message = "Неверные входные данные",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Created("", errorMsg);


                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }



        [HttpPost]
        [Route("api/account/refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] tokenInfoDTO model)
        {
            // Проверяем, что Refresh токен был предоставлен
            if (string.IsNullOrEmpty(model?.refreshToken))
            {
                return BadRequest("Не предоставлен Refresh токен");
            }

            // Находим пользователя по Refresh токену
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == model.refreshToken);
            if (user == null)
            {
                return BadRequest("Неверный Refresh токен");
            }

            // Проверяем срок действия Refresh токена
            if (DateTime.UtcNow > user.RefreshTokenExpiryTime)
            {
                return BadRequest("Истек срок действия Refresh токена");
            }

            // Генерируем новый JWT токен
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var newToken = _tokenServices.GenerateAccessToken(authClaims);
            var newRefreshToken = _tokenServices.GenerateRefreshToken();


            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddYears(1);
            await _userManager.UpdateAsync(user);
            return Ok(new newUserDTO
            {
                Token = newToken,
                RefreshToken = newRefreshToken
            });
        }



    }
}

//{
//    "phone": "+79999999998",
//  "password": "QWEewq123@",
//  "passwordConfirm": "QWEewq123@"
//}