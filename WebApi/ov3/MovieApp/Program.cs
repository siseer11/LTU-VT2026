using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using MovieApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(
	dbContextOptions => dbContextOptions.UseSqlServer(builder.Configuration.GetConnectionString("db"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();
}

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

	await SeedData.Initialize(context);
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
