
using E_Commerce_API.Dtos.ProductDtos;
using E_Commerce_API.Models;

namespace E_Commerce_API.Services.ProductService
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProduts();

        Task<Product?> GetById(int id);

        Task<ProductDto?> GetByIdDetails(int id);

        Task<Product> CreateProuct(Product product);

        Task UpdateProuct(Product product);

        Task DeleteProuct(Product product);

        Task<List<ProductDto>?> GetProductsBycategory(int id);

        Task<List<Product>?> SearchByPartOfName(string Name);

        Task<bool> HasProductsInCategory(int categoryId);
    }
}
