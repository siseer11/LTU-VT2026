using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageApi.DbContexts;
using StorageApi.Models;

namespace StorageApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
	private readonly ProductInfoContext _context;

	public ProductController(ProductInfoContext context)
	{
		_context = context;
	}


	[HttpGet()]
	public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
	{
		var products = await _context.Products.ToListAsync();

		return Ok(products);
	}

	[HttpGet("{id:int}", Name = "GetProductById")]
	public async Task<ActionResult<Product>> GetProduct(int id)
	{
		var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);

		if (product is null)
			return NotFound();

		return Ok(product);
	}

	[HttpPost]
	public async Task<ActionResult> CreateProduct([FromBody] ProductCreationDto newProduct)
	{
		var res = await _context.Products.AddAsync(new Entities.Product(newProduct.Name, newProduct.Category, newProduct.Price)
		{
			Count = newProduct.Count,
			Description = newProduct.Description,
			ExpirationDate = newProduct.ExpirationDate,
			Shelf = newProduct.Shelf
		});
		await _context.SaveChangesAsync();

		return CreatedAtRoute("GetProductById", new { id = res.Entity.Id }, res.Entity);
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteProduct(int id)
	{
		var obj = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);

		if (obj is null)
			return NotFound();

		_context.Products.Remove(obj);
		await _context.SaveChangesAsync();

		return NoContent();
	}

}

