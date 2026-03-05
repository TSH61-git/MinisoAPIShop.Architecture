using Entities;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyWebApiShopContext _context;

        public ProductRepository(MyWebApiShopContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var existing = await _context.Products.FindAsync(product.ProductId);
            if (existing == null)
                return false;

            existing.ProductName = product.ProductName;
            existing.Price = product.Price;
            existing.Description = product.Description;
            existing.CategoryId = product.CategoryId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(List<Product> Items, int TotalCount)> GetProducts(int position, int skip, ProductSearchParams? parameters)
        {

            var query = _context.Products.Where(product =>
            ((parameters.Desc == null) ? (true) : (product.Description.Contains(parameters.Desc)))
            && ((parameters.MinPrice == null) ? (true) : (product.Price >= parameters.MinPrice))
            && ((parameters.MaxPrice == null) ? (true) : (product.Price <= parameters.MaxPrice))
            && ((parameters.CategoryIDs == null || parameters.CategoryIDs.Count == 0) ? (true) : (parameters.CategoryIDs.Contains((int)product.CategoryId))))
            .OrderBy(product => product.Price);

            if (position < 1) position = 1;
            if (skip < 1) skip = 10; // או ערך ברירת מחדל אחר

            List<Product> products = await query.Skip((position - 1) * skip)
            .Take(skip).Include(product => product.Category).ToListAsync();
            var total = await query.CountAsync();
            return (products, total);
        }

        public async Task<Product?> GetProductById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            return product;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ProductHasOrdersAsync(int productId)
        {
            return await _context.OrderItems
                .AnyAsync(o => o.ProductId == productId);
        }
    }
}
