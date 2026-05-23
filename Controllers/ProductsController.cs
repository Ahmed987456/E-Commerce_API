using E_Commerce_API.Dtos.ProductDtos;
using E_Commerce_API.Models;
using E_Commerce_API.Services.CategoryServices;
using E_Commerce_API.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productSercive;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productSercive, IMapper mapper, ICategoryService categoryService)
        {
            _productSercive = productSercive;
            _mapper = mapper;
            _categoryService = categoryService;
        }


        #region GET

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productSercive.GetAllProduts();
            return Ok(products);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var product = await _productSercive.GetByIdDetails(id);
            if (product == null)
                return NotFound("Not Product Found With this id");
            return Ok(product);
        }

        [HttpGet("productsByCategory/{id}")]

        public async Task<IActionResult> GetProductsBycategoryAsync(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null)
                return NotFound("Not Category with this id");
            var products = await _productSercive.GetProductsBycategory(id);
            return Ok(products);
        }

        [HttpGet("search")]

        public async Task<IActionResult> SearchByPartOfNameAsync(string Name)
        {
            var products = await _productSercive.SearchByPartOfName(Name);
            if (!products.Any())
                return NotFound("No products found with this name");
            return Ok(products);
        }

        #endregion


        #region Create

        [HttpPost]
        public async Task<IActionResult> CreatAsync([FromForm] CreateProductDto dto)
        {
            var category = await _categoryService.GetById(dto.CategoryId);

            if (category == null)
                return NotFound("Not category Found with this id");

            var product = _mapper.Map<Product>(dto);

            await _productSercive.CreateProuct(product);

            product.Category = category;

            return Ok(_mapper.Map<ProductDto>(product));
        }

        #endregion

        #region Update

        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromForm] UpdateProductDto dto)
        {
            var product = await _productSercive.GetById(id);
            if (product == null)
                return NotFound("Not Product Found With This ID");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                product.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.Description))
                product.Description = dto.Description;

            if (dto.Price.HasValue)
                product.Price = dto.Price.Value;

            if (dto.StockQuantity.HasValue)
                product.StockQuantity = dto.StockQuantity.Value;

            if (!string.IsNullOrEmpty(dto.ImageUrl))
                product.ImageUrl = dto.ImageUrl;

            if (dto.CategoryId.HasValue)
            {
             var category = await _categoryService.GetById(dto.CategoryId.Value);

                if (category == null)
                    return NotFound("Not category Found");

                product.CategoryId = dto.CategoryId.Value;
            }
         
            await _productSercive.UpdateProuct(product);

            return Ok(_mapper.Map<ProductDto>(product));
        }

        #endregion

        #region Delete

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var product = await _productSercive.GetById(id);
            if (product == null)
                return NotFound("Not Product Found With This ID");
            await _productSercive.DeleteProuct(product);
            return NoContent();
        }

        #endregion

    }
}
