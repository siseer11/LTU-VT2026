using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microshop.DatabaseFirstApi.Models;
using Microshop.DatabaseFirstApi.Dtos;

namespace Microshop.DatabaseFirstApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
	private readonly OrderDbContext _context;

	public CustomersController(OrderDbContext context)
	{
		_context = context;
	}

	// GET: api/customers
	[HttpGet]
	public async Task<ActionResult<IEnumerable<CustomerSummaryDto>>> GetAllCustomers()
	{
		var customers = await _context.Customers
				.OrderBy(c => c.CustomerId)
				.Select(c => new CustomerSummaryDto(c.FirstName, c.LastName, c.Email, c.Phone))
				.ToListAsync();

		return Ok(customers);
	}

	// GET: api/customers/5 (med Orders inkluderade)
	[HttpGet("{id}")]
	public async Task<ActionResult<Customer>> GetCustomer(int id)
	{
		var customer = await _context.Customers
			.Where(c => c.CustomerId == id)
			.Select(c => new CustomerDetailsDto(
					c.FirstName,
					c.LastName,
					c.Email,
					c.Phone,
					c.Orders.Select(o => new OrderDto(o.OrderDate, o.TotalAmount, o.Status)).ToList()
				)
			)
			.FirstOrDefaultAsync();

		if (customer == null)
		{
			return NotFound();
		}

		return Ok(customer);
	}

	// POST: api/customers
	[HttpPost]
	public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreationDto customerCreationBody)
	{
		// Obs: CustomerId är IDENTITY i databasen, så vi ska inte sätta den manuellt
		var newDbCustomer = _context.Customers.Add(new Customer()
		{
			CreatedAt = new DateTime(),
			Email = customerCreationBody.Email,
			FirstName = customerCreationBody.FirstName,
			LastName = customerCreationBody.LastName,
			Phone = customerCreationBody.Phone
		});
		await _context.SaveChangesAsync();

		Customer customer = newDbCustomer.Entity;
		// Returnera den skapade kundens data (inklusive ny CustomerId)
		return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
	}

	// PUT: api/customers/5
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
	{
		if (id != customer.CustomerId)
		{
			return BadRequest("CustomerId måste matcha id i URL.");
		}

		// Hämta befintlig kund
		var existingCustomer = await _context.Customers
				.FirstOrDefaultAsync(c => c.CustomerId == id);

		if (existingCustomer == null)
		{
			return NotFound();
		}

		// Uppdatera egenskaper
		existingCustomer.FirstName = customer.FirstName;
		existingCustomer.LastName = customer.LastName;
		existingCustomer.Email = customer.Email;
		existingCustomer.Phone = customer.Phone;

		await _context.SaveChangesAsync();

		return NoContent();
	}

	// DELETE: api/customers/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteCustomer(int id)
	{
		var customer = await _context.Customers
				.FirstOrDefaultAsync(c => c.CustomerId == id);

		if (customer == null)
		{
			return NotFound();
		}

		_context.Customers.Remove(customer);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}