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
    public class AdvertController : ControllerBase
    {
        
        private readonly IlanContext _context;

        public AdvertController(IlanContext context)
        {
           _context = context;
        }

        // localhost:5000/api/products => GET
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Advert.Where(i => i.IsActive).Select(p => 
            ProductToDTO(p))
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
                .Advert.Where(i => i.AdvertId == id)
                .Select(p => ProductToDTO(p))
                .FirstOrDefaultAsync();

            if(p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Advert entity)
        {
            _context.Advert.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = entity.AdvertId }, entity);
        }

        // localhost:5000/api/products/5 => GET
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Advert entity)
        {
            if (id != entity.AdvertId)
            {
                return BadRequest();
            }

            var product = await _context.Advert.FirstOrDefaultAsync(i => i.AdvertId == id);

            if (product == null)
            {
                return NotFound();
            }

            product.AdvertTitle = entity.AdvertTitle;
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

            var product = await _context.Advert.FirstOrDefaultAsync(i => i.AdvertId == id);

            if(product == null)
            {
                return NotFound();
            }

            _context.Advert.Remove(product);

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
        private static AdvertDTO ProductToDTO(Advert p)
        {
            var entity = new AdvertDTO();
            if(p != null)
            {
                entity.AdvertId = p.AdvertId;
                entity.AdvertTitle = p.AdvertTitle;
                entity.Date = p.Date;
            }
            return entity;
        }
    }


}