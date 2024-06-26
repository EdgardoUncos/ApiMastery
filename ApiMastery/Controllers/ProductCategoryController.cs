﻿using ApiMastery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiMastery.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _context;

        public ProductCategoryController(APIFurnitureStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductCategory>> Get()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var productCategory = await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (productCategory == null)
            {
                return NotFound();
            }

            return Ok(productCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductCategory productCategory)
        {
            await _context.ProductCategories.AddAsync(productCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Post", productCategory.Id, productCategory);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ProductCategory productCategory)
        {
            if (productCategory == null) return NotFound();

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
