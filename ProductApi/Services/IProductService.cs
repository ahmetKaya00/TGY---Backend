using ProductApi.DTOs;

namespace ProductApi.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>>GetAllAsync();
        Task<ProductResponse?>GetByIdAsync(int id);
        Task<ProductResponse>CreateAsync(CreateProductRequest request);
        Task<bool>UpdateAsync(int id,UpdateProductRequest request);
        Task<bool>DeleteAsync(int id);
    }
}