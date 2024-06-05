using EmlakPortali.DTO;
using EmlakPortali.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace EmlakPortali.Controllers
{
    // localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ForRentController : ControllerBase
    {
        
        private readonly IlanContext _context;

        public ForRentController(IlanContext context)
        {
           _context = context;
        }

        // localhost:5000/api/products => GET
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.ForRent.Where(i => i.IsActive).Select(p => 
            KiralikDTO(p))
            .ToListAsync();
            return Ok(products);
        }

        // localhost:5000/api/products/1 => GET
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
           if(id == null)
            {
                return NotFound();
            }

            var p = await _context
                .ForRent.Where(i => i.ForRentId == id)
                .Select(p => KiralikDTO(p))
                .FirstOrDefaultAsync();

            if(p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ForRent entity)
        {
            _context.ForRent.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = entity.ForRentId }, entity);
        }

        // localhost:5000/api/products/5 => GET
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ForRent entity)
        {
            if (id != entity.ForRentId)
            {
                return BadRequest();
            }

            var product = await _context.ForRent.FirstOrDefaultAsync(i => i.ForRentId == id);

            if (product == null)
            {
                return NotFound();
            }

            product.ForRentTitle = entity.ForRentTitle;
            product.Date = entity.Date;
            product.IsActive = entity.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if(id == null) 
            { 
                return NotFound();
            }

            var product = await _context.ForRent.FirstOrDefaultAsync(i => i.ForRentId == id);

            if(product == null)
            {
                return NotFound();
            }

            _context.ForRent.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return NotFound();
            }
            return NoContent();

        }
        private static AdvertDTO KiralikDTO(ForRent p)
        {
            var entity = new AdvertDTO();
            if(p != null)
            {
                entity.AdvertId = p.ForRentId;
                entity.AdvertTitle = p.ForRentTitle;
                entity.Date = p.Date;
            }
            return entity;
        }
    }


}