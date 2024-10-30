using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pizzaServerApp.Models;

namespace pizzaServerApp.Controllers
{
    [EnableCors]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private IDbCRUD _dbCRUD;
        public CouponController(IDbCRUD dbCRUD)
        {
            _dbCRUD = dbCRUD;
        }

        // GET: ComboProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<couponDTO>>> GetCoupons()
        {
            try
            {
                var Coupon = await Task.Run(() => _dbCRUD.couponDTOs);
                return Ok(Coupon);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: ComboProducts/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<couponDTO>> GetCoupon(int id)
        {
            var ComboProduct = await Task.Run(() => _dbCRUD.couponDTOs.FirstOrDefault(i => i.Id == id));

            if (ComboProduct == null)
            {
                return NotFound();
            }

            return Ok(ComboProduct);
        }

        // GET: ComboProducts/Create
        [HttpPost]
        public async Task<ActionResult<couponDTO>> PostCoupon(couponDTO Coupon)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            try
            {
                _dbCRUD.AddCoupon(Coupon);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction("GetCoupon", new { id = Coupon.Id }, Coupon);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutComboProduct(int id, couponDTO Coupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            if (id != Coupon.Id)
            {
                return BadRequest("Mismatched id");
            }

            try
            {
                _dbCRUD.UpdateCoupon(Coupon);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                if (!_dbCRUD.couponDTOs.Any(i => i.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    BadRequest(e.Message);
                }
            }

            return CreatedAtAction("GetCoupon", new { id = Coupon.Id }, Coupon);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var doctor = await Task.Run(() => _dbCRUD.couponDTOs.Find(i => i.Id == id));
            if (doctor == null)
            {
                return NotFound($"Not found id {id}");
            }

            _dbCRUD.DeleteCoupon(id);
            await _dbCRUD.Save();

            return Ok();
        }

    }
}
