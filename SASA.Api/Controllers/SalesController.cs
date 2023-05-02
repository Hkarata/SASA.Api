using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SASA.Api.Data;
using SASA.Api.Models;

namespace SASA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SalesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<SoldProduct>> AddSale(SoldProduct soldProduct)
        {
            var product = _context.Products.SingleOrDefault(i => i.Name == soldProduct.Name);
            if (product?.Amount > soldProduct.Amount)
            {
                product.Amount -= soldProduct.Amount;
                _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SoldProducts.Add(soldProduct);
                await _context.SaveChangesAsync();
                return Ok(soldProduct);
            }
            else
            {
                return NoContent();
            }
        }

    }
}
