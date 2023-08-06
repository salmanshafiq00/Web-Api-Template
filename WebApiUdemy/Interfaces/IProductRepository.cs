using WebApiUdemy.Model;

namespace WebApiUdemy.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetAllAsync(int? categoryId);
        Task<Product?> GetAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product?> UpdateAsync(int id, Product product);
        Task<Product?> DeleteAsync(int id);
        Task<List<Product>?> GetAllWithCategoryAsync();
    }
}
