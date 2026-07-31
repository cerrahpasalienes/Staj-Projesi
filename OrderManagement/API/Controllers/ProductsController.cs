using Application.DTOs.Products;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound(new { Message = "Ürün bulunamadı." });
        
        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequestDto request)
    {
        var productId = await _productService.CreateProductAsync(request);
        return Ok(new { Message = "Ürün başarıyla oluşturuldu.", ProductId = productId });
    }

    // PUT: api/products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequestDto request)
    {
        try
        {
            await _productService.UpdateProductAsync(id, request);
            return Ok(new { Message = "Ürün başarıyla güncellendi." });
        }
        catch (Exception ex)
        {
            // İleride Global Exception Middleware yazdığımızda bu try-catch'lere gerek kalmayacak
            return BadRequest(new { Message = ex.Message });
        }
    }

    // DELETE: api/products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
            return Ok(new { Message = "Ürün başarıyla silindi." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}