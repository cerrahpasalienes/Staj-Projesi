using Application.DTOs.Customers;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    // GET: api/customers
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    // GET: api/customers/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound(new { Message = "Müşteri bulunamadı." });
        
        return Ok(customer);
    }

    // POST: api/customers
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequestDto request)
    {
        var customerId = await _customerService.CreateCustomerAsync(request);
        return Ok(new { Message = "Müşteri başarıyla oluşturuldu.", CustomerId = customerId });
    }

    // PUT: api/customers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerRequestDto request)
    {
        try
        {
            await _customerService.UpdateCustomerAsync(id, request);
            return Ok(new { Message = "Müşteri başarıyla güncellendi." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    // DELETE: api/customers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _customerService.DeleteCustomerAsync(id);
            return Ok(new { Message = "Müşteri başarıyla silindi." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}