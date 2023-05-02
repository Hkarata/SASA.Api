using SASA.Api.Data;
using SASA.Api.Models;

namespace SASA.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AutoPopulateTableAsync()
        {
            var data = _context.StockProducts.GroupBy(x => new {x.Name, x.Description, x.Unit, x.Price})
                .Select(x => new
                {
                    Name = x.Key.Name,
                    Description = x.Key.Description,
                    Unit = x.Key.Unit,
                    Price = x.Key.Price,
                    Amount = x.Sum(y => y.Amount)
                }).ToList();

            foreach (var item in data)
            {
                var record = _context.Products.SingleOrDefault(x => x.Name == item.Name && x.Description == item.Description && x.Unit == item.Unit && x.Price == item.Price);

                if (record != null)
                {
                    record.Amount += item.Amount;
                }

                else
                {
                    record = new Product
                    {
                        Name = item.Name,
                        Description = item.Description,
                        Unit = item.Unit,
                        Price = item.Price,
                        Amount = item.Amount
                    };
                    _context.Products.Add(record);
                }
             
            }

            await _context.SaveChangesAsync();

        }
    }
}
