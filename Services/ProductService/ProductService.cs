
using E_Commerce_API.Dtos.ProductDtos;

namespace E_Commerce_API.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<ProductDto>> GetAllProduts()
        {
            var products= await _context.Products.Include(s=>s.Category).ToListAsync();

            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products.Include(c=>c.Category).SingleOrDefaultAsync(s=>s.Id== id);
        }

        public async Task<Product> CreateProuct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateProuct(Product product)
        {
            _context.Products.Update(product);
             await _context.SaveChangesAsync();
        }

        public async Task DeleteProuct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductDto>?> GetProductsBycategory(int id)
        {
           var products = await _context.Products.Where(s => s.CategoryId == id).ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<List<Product>?> SearchByPartOfName(string Name)
        {
          return await _context.Products.Where(s => s.Name.ToLower().Contains(Name.ToLower())).ToListAsync();
        }

        public async Task<bool> HasProductsInCategory(int categoryId)
        {
            return await _context.Products.AnyAsync(s => s.CategoryId == categoryId);
        }

        public async Task<ProductDto?> GetByIdDetails(int id)
        {
            var product= await _context.Products.Include(s => s.Category).SingleOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<ProductDto>(product);
        }
    }
}
