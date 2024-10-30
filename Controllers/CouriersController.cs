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
    public class CourierController : ControllerBase
    {
        private IDbCRUD _dbCRUD;
        public CourierController(IDbCRUD dbCRUD)
        {
            _dbCRUD = dbCRUD;
        }

        // GET: ComboProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<courierDTO>>> GetCouriers()
        {
            try
            {
                var Courier = await Task.Run(() => _dbCRUD.courierDTOs);
                return Ok(Courier);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: ComboProducts/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<courierDTO>> GetCourier(int id)
        {
            var ComboProduct = await Task.Run(() => _dbCRUD.courierDTOs.FirstOrDefault(i => i.Id == id));

            if (ComboProduct == null)
            {
                return NotFound();
            }

            return Ok(ComboProduct);
        }

        // GET: ComboProducts/Create
        [HttpPost]
        public async Task<ActionResult<courierDTO>> PostCourier(courierDTO Courier)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            try
            {
                _dbCRUD.AddCourier(Courier);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction("GetCourier", new { id = Courier.Id }, Courier);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutComboProduct(int id, courierDTO Courier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            if (id != Courier.Id)
            {
                return BadRequest("Mismatched id");
            }

            try
            {
                _dbCRUD.UpdateCourier(Courier);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                if (!_dbCRUD.courierDTOs.Any(i => i.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    BadRequest(e.Message);
                }
            }

            return CreatedAtAction("GetCourier", new { id = Courier.Id }, Courier);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourier(int id)
        {
            var doctor = await Task.Run(() => _dbCRUD.courierDTOs.Find(i => i.Id == id));
            if (doctor == null)
            {
                return NotFound($"Not found id {id}");
            }

            _dbCRUD.DeleteCourier(id);
            await _dbCRUD.Save();

            return Ok();
        }

    }
}
