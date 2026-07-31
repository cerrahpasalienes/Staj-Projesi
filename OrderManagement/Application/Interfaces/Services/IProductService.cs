using Application.DTOs.Products;

namespace Application.Interfaces.Services;

public interface IProductService
{
    Task<IReadOnlyList<ProductListDto>> GetAllProductsAsync();
    Task<ProductListDto?> GetProductByIdAsync(int id); // YENİ: Tekil getirme
    Task<int> CreateProductAsync(CreateProductRequestDto request);
    Task UpdateProductAsync(int id, UpdateProductRequestDto request); // YENİ: Güncelleme
    Task DeleteProductAsync(int id); // YENİ: Silme
}