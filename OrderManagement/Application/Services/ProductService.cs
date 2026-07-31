using Application.DTOs.Products;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IQueryRepository<Product> _queryRepository;
    private readonly ICommandRepository<Product> _commandRepository;

    public ProductService(IQueryRepository<Product> queryRepository, ICommandRepository<Product> commandRepository)
    {
        _queryRepository = queryRepository;
        _commandRepository = commandRepository;
    }

    public async Task<IReadOnlyList<ProductListDto>> GetAllProductsAsync()
    {
        var products = await _queryRepository.GetAllAsync();
        
        return products.Select(p => new ProductListDto
        {
            Id = p.Id,
            ProductCode = p.ProductCode,
            Name = p.Name,
            Price = p.Price,
            StockQuantity = p.StockQuantity
        }).ToList();
    }

    public async Task<int> CreateProductAsync(CreateProductRequestDto request)
    {
        var product = new Product
        {
            ProductCode = request.ProductCode,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var createdProduct = await _commandRepository.AddAsync(product);
        return createdProduct.Id;
    }

    // YENİ EKLENEN METOT: ID'ye Göre Getir
    public async Task<ProductListDto?> GetProductByIdAsync(int id)
    {
        var product = await _queryRepository.GetByIdAsync(id);
        if (product == null) return null;

        return new ProductListDto
        {
            Id = product.Id,
            ProductCode = product.ProductCode,
            Name = product.Name,
            Price = product.Price,
            StockQuantity = product.StockQuantity
        };
    }

    // YENİ EKLENEN METOT: Güncelleme
    public async Task UpdateProductAsync(int id, UpdateProductRequestDto request)
    {
        var product = await _queryRepository.GetByIdAsync(id);
        if (product == null)
            throw new Exception("Ürün bulunamadı!");

        product.ProductCode = request.ProductCode;
        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.StockQuantity = request.StockQuantity;
        product.IsActive = request.IsActive;
        product.UpdatedAt = DateTime.UtcNow;

        await _commandRepository.UpdateAsync(product);
    }

    // YENİ EKLENEN METOT: Silme
    public async Task DeleteProductAsync(int id)
    {
        var product = await _queryRepository.GetByIdAsync(id);
        if (product == null)
            throw new Exception("Ürün bulunamadı!");

        await _commandRepository.DeleteAsync(product);
    }
}