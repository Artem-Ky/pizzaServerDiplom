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
    public class CustomProductController : ControllerBase
    {
        private IDbCRUD _dbCRUD;
        public CustomProductController(IDbCRUD dbCRUD)
        {
            _dbCRUD = dbCRUD;
        }

        // GET: ComboProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<customProductDTO>>> GetCustomProducts()
        {
            try
            {
                var CustomProduct = await Task.Run(() => _dbCRUD.customProductDTOs);
                return Ok(CustomProduct);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: ComboProducts/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<customProductDTO>> GetCustomProduct(int id)
        {
            var ComboProduct = await Task.Run(() => _dbCRUD.customProductDTOs.FirstOrDefault(i => i.Id == id));

            if (ComboProduct == null)
            {
                return NotFound();
            }

            return Ok(ComboProduct);
        }

        // GET: ComboProducts/Create
        [HttpPost]
        public async Task<ActionResult<customProductDTO>> PostCustomProduct(customProductDTO CustomProduct)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            try
            {
                _dbCRUD.AddCustomProduct(CustomProduct);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction("GetCustomProduct", new { id = CustomProduct.Id }, CustomProduct);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomProduct(int id, customProductDTO CustomProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            if (id != CustomProduct.Id)
            {
                return BadRequest("Mismatched id");
            }

            try
            {
                _dbCRUD.UpdateCustomProduct(CustomProduct);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                if (!_dbCRUD.customProductDTOs.Any(i => i.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    BadRequest(e.Message);
                }
            }

            return CreatedAtAction("GetCustomProduct", new { id = CustomProduct.Id }, CustomProduct);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomProduct(int id)
        {
            var doctor = await Task.Run(() => _dbCRUD.customProductDTOs.Find(i => i.Id == id));
            if (doctor == null)
            {
                return NotFound($"Not found id {id}");
            }

            _dbCRUD.DeleteCustomProduct(id);
            await _dbCRUD.Save();

            return Ok();
        }

    }
}
