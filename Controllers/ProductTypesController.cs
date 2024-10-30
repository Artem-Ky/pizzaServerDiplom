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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private IDbCRUD _dbCRUD;
        public ProductTypeController(IDbCRUD dbCRUD)
        {
            _dbCRUD = dbCRUD;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<productTypeDTO>>> GetProductTypes()
        {
            try
            {
                var ProductTypes = await Task.Run(() => _dbCRUD.productTypeDTOs);
                return Ok(ProductTypes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: ProductTypes/Details/5
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<productTypeDTO>> GetProductType(int id)
        {
            var ProductType = await Task.Run(() => _dbCRUD.productTypeDTOs.FirstOrDefault(i => i.Id == id));

            if (ProductType == null)
            {
                return NotFound();
            }

            return Ok(ProductType);
        }

        // GET: ProductTypes/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<productTypeDTO>> PostProductType(productTypeDTO ProductType)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            try
            {
                ProductType.Id = _dbCRUD.AddProductType(ProductType);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction("GetProductType", new { id = ProductType.Id }, ProductType);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductTypes(int id, productTypeDTO ProductType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            if (id != ProductType.Id)
            {
                return BadRequest("Mismatched id");
            }

            try
            {
                _dbCRUD.UpdateProductType(ProductType);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                if (!_dbCRUD.productTypeDTOs.Any(i => i.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    BadRequest(e.Message);
                }
            }

            return CreatedAtAction("GetProductType", new { id = ProductType.Id }, ProductType);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductType(int id)
        {
            var doctor = await Task.Run(() => _dbCRUD.productTypeDTOs.Find(i => i.Id == id));
            if (doctor == null)
            {
                return NotFound($"Not found id {id}");
            }

            _dbCRUD.DeleteProductType(id);
            await _dbCRUD.Save();

            return Ok();
        }

    }
}
