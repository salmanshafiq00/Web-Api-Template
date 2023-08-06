using Microsoft.EntityFrameworkCore;
using WebApiUdemy.Data;
using WebApiUdemy.Interfaces;
using WebApiUdemy.Model;

namespace WebApiUdemy.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(int id)
        {

            var category = await _context.Categories.FindAsync(id);
            if (category is null)
            {
                return null;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory is null)
            {
                return null;
            }
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.PhotoUrl = category.PhotoUrl;
            await _context.SaveChangesAsync();
            return existingCategory; ;
        }

        public async Task<Category?> GetAsync(int id) => await _context.Categories.FindAsync(id);

        public async Task<List<Category>> GetAllAsync() => await _context.Categories.ToListAsync();
    }
}
