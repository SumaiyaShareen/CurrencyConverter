using CurrencyConverter.Models;
using CurrencyConverterApi.Interfaces;   // ✅ Fix namespace (matches your interfaces)
using CurrencyConverterApi.Repositories; // ✅ Add this so DI finds the repositories
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register DbContext with connection string
builder.Services.AddDbContext<CurrencyConverterContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Register Repositories (Dependency Injection)
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
builder.Services.AddScoped<IConversionHistoryRepository, ConversionHistoryRepository>();

// ✅ Enable CORS for frontend (Angular, React, etc.)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Enable CORS middleware
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
