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
    public class ComboProductsController : ControllerBase
    {
        private IDbCRUD _dbCRUD;
        public ComboProductsController(IDbCRUD dbCRUD)
        {
            _dbCRUD = dbCRUD;
        }

        // GET: ComboProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<comboProductDTO>>> GetComboProducts()
        {
            try
            {
                var ComboProducts = await Task.Run(() => _dbCRUD.comboProductDTOs);
                return Ok(ComboProducts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: ComboProducts/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<comboProductDTO>> GetComboProduct(int id)
        {
            var ComboProduct = await Task.Run(() => _dbCRUD.comboProductDTOs.FirstOrDefault(i => i.Id == id));

            if (ComboProduct == null)
            {
                return NotFound();
            }

            return Ok(ComboProduct);
        }

        // GET: ComboProducts/Create
        [HttpPost]
        public async Task<ActionResult<comboProductDTO>> PostComboProduct(comboProductDTO ComboProduct)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            try
            {
                _dbCRUD.AddComboProduct(ComboProduct);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction("GetComboProduct", new { id = ComboProduct.Id }, ComboProduct);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutComboProduct(int id, comboProductDTO ComboProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            if (id != ComboProduct.Id)
            {
                return BadRequest("Mismatched id");
            }

            try
            {
                _dbCRUD.UpdateComboProduct(ComboProduct);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                if (!_dbCRUD.comboProductDTOs.Any(i => i.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    BadRequest(e.Message);
                }
            }

            return CreatedAtAction("GetComboProduct", new { id = ComboProduct.Id }, ComboProduct);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComboProduct(int id)
        {
            var doctor = await Task.Run(() => _dbCRUD.comboProductDTOs.Find(i => i.Id == id));
            if (doctor == null)
            {
                return NotFound($"Not found id {id}");
            }

            _dbCRUD.DeleteComboProduct(id);
            await _dbCRUD.Save();

            return Ok();
        }

    }
}
