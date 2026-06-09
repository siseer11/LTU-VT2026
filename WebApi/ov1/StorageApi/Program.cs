using Microsoft.EntityFrameworkCore;
using StorageApi.DbContexts;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers(options =>
{
	options.ReturnHttpNotAcceptable = true; // will reurn an error if any other format than the supported one is requested. By default it will return json for every type of request
});

builder.Services.AddDbContext<ProductInfoContext>(dbContextOptions => dbContextOptions.UseSqlServer(builder.Configuration.GetConnectionString("ProductInfoDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
