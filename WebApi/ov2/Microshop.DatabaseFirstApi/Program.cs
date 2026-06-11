using Microshop.DatabaseFirstApi.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers(options =>
{
	options.ReturnHttpNotAcceptable = true; // will reurn an error if any other format than the supported one is requested. By default it will return json for every type of request
});

// Registrera OrderDbContext med SQL Server
var connectionString =
builder.Configuration.GetConnectionString("OrderDbConnection");
builder.Services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
