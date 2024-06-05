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
    public class ForSaleController : ControllerBase
    {
        
        private readonly IlanContext _context;

        public ForSaleController(IlanContext context)
        {
           _context = context;
        }

        // localhost:5000/api/products => GET
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.ForSale.Where(i => i.IsActive).Select(p => 
            SatilikDTO(p))
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
                .ForSale.Where(i => i.ForSaleId == id)
                .Select(p => SatilikDTO(p))
                .FirstOrDefaultAsync();

            if(p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ForSale entity)
        {
            _context.ForSale.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = entity.ForSaleId }, entity);
        }

        // localhost:5000/api/products/5 => GET
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ForSale entity)
        {
            if (id != entity.ForSaleId)
            {
                return BadRequest();
            }

            var product = await _context.ForSale.FirstOrDefaultAsync(i => i.ForSaleId == id);

            if (product == null)
            {
                return NotFound();
            }

            product.ForSaleTitle = entity.ForSaleTitle;
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

            var product = await _context.ForSale.FirstOrDefaultAsync(i => i.ForSaleId == id);

            if(product == null)
            {
                return NotFound();
            }

            _context.ForSale.Remove(product);

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
        private static AdvertDTO SatilikDTO(ForSale p)
        {
            var entity = new AdvertDTO();
            if(p != null)
            {
                entity.AdvertId = p.ForSaleId;
                entity.AdvertTitle = p.ForSaleTitle;
                entity.Date = p.Date;
            }
            return entity;
        }
    }


}