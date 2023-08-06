using Microsoft.EntityFrameworkCore;
using WebApiUdemy.Data;
using WebApiUdemy.Interfaces;
using WebApiUdemy.Model;

namespace WebApiUdemy.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(int id)
        {

            var product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                return null;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct is null)
            {
                return null;
            }
            existingProduct.Title = product.Title;
            existingProduct.ShortDescription = product.ShortDescription;
            existingProduct.LongDescription = product.LongDescription;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.PhotoUrl = product.PhotoUrl;
            await _context.SaveChangesAsync();
            return existingProduct; ;
        }

        public async Task<Product?> GetAsync(int id) => await _context.Products.FindAsync(id);

        public async Task<List<Product>> GetAllAsync() => await _context.Products.ToListAsync();

        public async Task<List<Product>> GetAllAsync(int? categoryId)
        {
            if (categoryId is null)
                return await _context.Products.Include(p => p.Category).ToListAsync();
            else
                return await _context.Products.Where(p => p.CategoryId == categoryId).Include(p => p.Category).ToListAsync();
        }

        public async Task<List<Product>?> GetAllWithCategoryAsync() 
            => await _context.Products.Include(p => p.Category).ToListAsync();

    }
}
