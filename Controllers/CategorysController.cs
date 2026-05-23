using E_Commerce_API.Dtos.CategoryDtos;
using E_Commerce_API.Services.CategoryServices;
using E_Commerce_API.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public CategorysController(ICategoryService categoryService, IMapper mapper, IProductService productService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _productService = productService;
        }
        
        #region Get

        [HttpGet]

        public async Task<IActionResult> GetAllAsync() 
        {
         var Categories = await _categoryService.GetAllCategories();
            return Ok(Categories);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var Categorie = await _categoryService.GetCategoryDetails(id);
            if (Categorie == null)
                return NotFound("Not Categorie With This Id");
            return Ok(Categorie);
        }

        #endregion

        #region Create

        [HttpPost]

        public async Task<IActionResult> CreateAsync([FromForm]CreateCategoryDto dto) 
        {
            var exists = await _categoryService.CategoryExists(dto.Name);
            if (exists)
                return BadRequest("Category already exists");
            var category = _mapper.Map<Category>(dto);
            await _categoryService.CreateCategory(category);
            return Ok(category);
        }

        #endregion

        #region Update
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateCategoryAsync(int id, [FromForm] UpdateCategoryDto dto) 
        {
          var category = await _categoryService.GetById(id);
            if(category == null)
                return NotFound("Not Category Found With This Id");
            var exist = await _categoryService.CategoryExistsForUpdate(dto.Name, id);
             if(exist)
                return BadRequest("The New Name Is Exists");
            await _categoryService.UpdateCategory(_mapper.Map(dto,category));  
            return Ok(new CategoryDto {
            Id = id,
            Name = dto.Name,
            });
         }

        #endregion

        #region Delete
        [HttpDelete]

        public async Task<IActionResult> DeleteCategoryAsync(int id) 
        {
         var category = await _categoryService.GetById(id);
            if (category == null)
                return NotFound("Not Category Found With This Id");
            var hasProducts = await _productService.HasProductsInCategory(id);
            if (hasProducts)
                return BadRequest("Cannot delete category because it contains products");
            await _categoryService.DeleteCategory(category);
            return NoContent();
        }

        #endregion

    }
}
