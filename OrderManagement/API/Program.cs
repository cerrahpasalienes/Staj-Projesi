using Infrastructure.Data.DataContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Controller desteğini ekliyoruz
builder.Services.AddControllers();

// 2. OpenAPI / Swagger desteği 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. DbContext'i Dependency Injection (DI) ile servislere ekliyoruz
builder.Services.AddDbContext<OrderManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 4. Generic Repository'leri DI (Dependency Injection) konteynerine ekliyoruz
builder.Services.AddScoped(typeof(Application.Interfaces.Repositories.ICommandRepository<>), typeof(Infrastructure.Repositories.Command.CommandRepository<>));
builder.Services.AddScoped(typeof(Application.Interfaces.Repositories.IQueryRepository<>), typeof(Infrastructure.Repositories.Query.QueryRepository<>));

// Application servislerini DI konteynerine ekliyoruz (builder.Build() işleminden ÖNCE olmalı)
builder.Services.AddScoped<Application.Interfaces.Services.IProductService, Application.Services.ProductService>();
builder.Services.AddScoped<Application.Interfaces.Services.ICustomerService, Application.Services.CustomerService>();
builder.Services.AddScoped<Application.Interfaces.Services.IOrderService, Application.Services.OrderService>(); // YENİ EKLENEN

builder.Services.AddScoped<Application.Interfaces.Services.IUserService, Application.Services.UserService>();

// TÜM SERVİS KAYITLARI BİTTİKTEN SONRA BUILD İŞLEMİ YAPILIR:
var app = builder.Build();


// HTTP Request Pipeline (İstek İşleme Hattı)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// İstekleri ilgili Controller'lara yönlendirir
app.MapControllers(); 

app.Run();