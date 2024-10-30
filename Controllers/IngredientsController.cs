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
    public class IngredientController : ControllerBase
    {
        private IDbCRUD _dbCRUD;
        public IngredientController(IDbCRUD dbCRUD)
        {
            _dbCRUD = dbCRUD;
        }

        // GET: ComboProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ingredientDTO>>> GetIngredients()
        {
            try
            {
                var Ingredient = await Task.Run(() => _dbCRUD.ingredientDTOs);
                return Ok(Ingredient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: ComboProducts/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ingredientDTO>> GetIngredient(int id)
        {
            var ComboProduct = await Task.Run(() => _dbCRUD.ingredientDTOs.FirstOrDefault(i => i.Id == id));

            if (ComboProduct == null)
            {
                return NotFound();
            }

            return Ok(ComboProduct);
        }

        // GET: ComboProducts/Create
        [HttpPost]
        public async Task<ActionResult<ingredientDTO>> PostIngredient(ingredientDTO Ingredient)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            try
            {
                _dbCRUD.AddIngredient(Ingredient);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction("GetIngredient", new { id = Ingredient.Id }, Ingredient);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutComboProduct(int id, ingredientDTO Ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            if (id != Ingredient.Id)
            {
                return BadRequest("Mismatched id");
            }

            try
            {
                _dbCRUD.UpdateIngredient(Ingredient);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                if (!_dbCRUD.ingredientDTOs.Any(i => i.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    BadRequest(e.Message);
                }
            }

            return CreatedAtAction("GetIngredient", new { id = Ingredient.Id }, Ingredient);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var doctor = await Task.Run(() => _dbCRUD.ingredientDTOs.Find(i => i.Id == id));
            if (doctor == null)
            {
                return NotFound($"Not found id {id}");
            }

            _dbCRUD.DeleteIngredient(id);
            await _dbCRUD.Save();

            return Ok();
        }

    }
}
