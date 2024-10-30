using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pizzaServerApp.Models;
using System.IdentityModel.Tokens.Jwt;


namespace pizzaServerApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class BannerTypeController : ControllerBase
    {
        private IDbCRUD _dbCRUD;
        public BannerTypeController(IDbCRUD dbCRUD)
        {
            _dbCRUD = dbCRUD;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<bannerTypeDTO>>> GetBannerTypes()
        {

            try
            {
                var BannerTypes = await Task.Run(() => _dbCRUD.bannerTypeDTOs);
                return Ok(BannerTypes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: BannerTypes/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<bannerTypeDTO>> GetBannerType(int id)
        {
            var BannerType = await Task.Run(() => _dbCRUD.bannerTypeDTOs.FirstOrDefault(i => i.Id == id));

            if (BannerType == null)
            {
                return NotFound();
            }

            return Ok(BannerType);
        }

        // GET: BannerTypes/Create
        [HttpPost]
        public async Task<ActionResult<bannerTypeDTO>> PostBannerType(bannerTypeDTO BannerType)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            try
            {
                BannerType.Id = _dbCRUD.AddBannerType(BannerType);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction("GetBannerType", new { id = BannerType.Id }, BannerType);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutBannerTypes(int id, bannerTypeDTO BannerType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            if (id != BannerType.Id)
            {
                return BadRequest("Mismatched id");
            }

            try
            {
                _dbCRUD.UpdateBannerType(BannerType);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                if (!_dbCRUD.bannerTypeDTOs.Any(i => i.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    BadRequest(e.Message);
                }
            }

            return CreatedAtAction("GetBannerType", new { id = BannerType.Id }, BannerType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBannerType(int id)
        {
            var doctor = await Task.Run(() => _dbCRUD.bannerTypeDTOs.Find(i => i.Id == id));
            if (doctor == null)
            {
                return NotFound($"Not found id {id}");
            }

            _dbCRUD.DeleteBannerType(id);
            await _dbCRUD.Save();

            return Ok();
        }


    }
}
