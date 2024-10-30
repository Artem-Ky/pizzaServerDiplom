using System;
using System.Collections.Generic;
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
    public class DefaultProductsController : ControllerBase
    {
        private IDbCRUD _dbCRUD;
        private IGetProducts _getProducts;
        public DefaultProductsController(IDbCRUD dbCRUD, IGetProducts getProducts)
        {
            _dbCRUD = dbCRUD;
            _getProducts = getProducts;
        }

        // GET: DefaultProducts
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<defaultProductDTO>>> GetDefaultProducts()
        {
            try
            {
                var defaultProducts = await Task.Run(() => _dbCRUD.defaultProductDTOs);
                return Ok(defaultProducts);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: DefaultProductsAndType
        [HttpGet("MenuList")]
        public async Task<ActionResult<IEnumerable<defaultProductDTO>>> GetMenuList()
        {
            try
            {
                var menuList = await Task.Run(() => _getProducts.menuListDTOs);
                return Ok(menuList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: DefaultProducts/Details/5
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<defaultProductDTO>> GetDefaultProduct(int id)
        {
            var defaultProduct = await Task.Run(() => _dbCRUD.defaultProductDTOs.FirstOrDefault(i => i.Id == id));

            if (defaultProduct == null)
            {
                return NotFound();
            }

            return Ok(defaultProduct);
        }

        // GET: DefaultProducts/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<defaultProductDTO>> PostDefaultProduct(defaultProductDTO defaultProduct)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            try
            {
                defaultProduct.Id = _dbCRUD.AddDefaultProduct(defaultProduct);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction("GetDefaultProduct", new { id = defaultProduct.Id }, defaultProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDefaultProduct(int id, defaultProductDTO defaultProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));
            }

            if (id != defaultProduct.Id)
            {
                return BadRequest("Mismatched id");
            }

            try
            {
                _dbCRUD.UpdateDefaultProduct(defaultProduct);
                await _dbCRUD.Save();
            }
            catch (Exception e)
            {
                if (!_dbCRUD.defaultProductDTOs.Any(i => i.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    BadRequest(e.Message);
                }
            }

            return CreatedAtAction("GetDefaultProduct", new { id = defaultProduct.Id }, defaultProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDefaultProduct(int id)
        {
            var doctor = await Task.Run(() => _dbCRUD.defaultProductDTOs.Find(i => i.Id == id));
            if (doctor == null)
            {
                return NotFound($"Not found id {id}");
            }

            _dbCRUD.DeleteDefaultProduct(id);
            await _dbCRUD.Save();

            return Ok();
        }

    }
}
