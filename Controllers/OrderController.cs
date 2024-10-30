using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using pizzaServerApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
namespace pizzaServerApp.Controllers
{

    [Route("api/[controller]")] 
    [ApiController] 
    [Authorize(Roles = "User")]
    [EnableCors]
    public class OrderController : ControllerBase
    {
        private readonly IDbCRUD _dbCRUD;
        private readonly UserManager<User> _userManager;


        public OrderController(IDbCRUD dbCRUD, UserManager<User> userManager)
        {
            _dbCRUD = dbCRUD;
            _userManager = userManager;
        }

        // POST: api/Order/Create
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] preOrderDTO orderDto)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var nameClaim = User.FindFirst(ClaimTypes.Name);
                var phoneNumber = nameClaim?.Value;
                var user = await _userManager.FindByNameAsync(phoneNumber);

                int orderId = await _dbCRUD.CreateOrder(orderDto, user.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
