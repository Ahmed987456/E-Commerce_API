using E_Commerce_API.Dtos.CategoryDtos;

namespace E_Commerce_API.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<CategoryWithCountDto>> GetAllCategories();

        Task<CategoryDetailsDto?> GetCategoryDetails(int id);

        Task<Category> CreateCategory(Category category);

        Task<bool> CategoryExists(string Name);

        Task<bool> CategoryExistsForUpdate(string Name, int id);

        Task<Category> UpdateCategory(Category category);

        Task DeleteCategory(Category category);

        Task<Category?> GetById(int id);
    }
}
