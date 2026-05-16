using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.DTOs;
using ProductApi.Models;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null || !product.IsActive)
            {
                return false;
            }

            product.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductResponse>> GetAllAsync()
        {
            return await _context.Products.Where(p=>p.IsActive).Select(p=>new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity
            }).ToListAsync();
        }

        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product == null || !product.IsActive)
            {
                return null;
            }

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductRequest request)
        {
            var product = await _context.Products.FindAsync(id);

            if(product == null || !product.IsActive)
            {
                return false;
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}