using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SASA.Api.Data;
using SASA.Api.DTOs;
using SASA.Api.Models;
using SASA.Api.Services;

namespace SASA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IProductService _pService;
        public StocksController(AppDbContext context, IProductService pService)
        {
            _context = context;
            _pService = pService;
        }

        [HttpPost]
        public async Task<ActionResult<List<Stock>>> AddStock(StockCreateDto request)
        {
            var newStock = new Stock
            {
                Date = request.Date,
                Price = request.Price,
                TransportFee = request.TransportFee
            };

            var products = request.Products.Select(p => new StockProduct
            {
                Name = p.Name,
                Description = p.Description,
                Amount = p.Amount,
                Unit = p.unit,
                Price = p.Price,
                Stock = newStock
            }).ToList();

            newStock.StockProducts = products;

            _context.Stocks.Add(newStock);
            await _context.SaveChangesAsync();

            await _pService.AutoPopulateTableAsync();

            return Ok(await _context.Stocks.Include(s => s.StockProducts).ToListAsync());


        }

    }
}
