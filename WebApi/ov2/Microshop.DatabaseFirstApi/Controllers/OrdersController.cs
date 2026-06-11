
using Microshop.DatabaseFirstApi.Dtos;
using Microshop.DatabaseFirstApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Microshop.DatabaseFirstApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class OrdersController : ControllerBase
{
	private readonly OrderDbContext _context;

	public OrdersController(OrderDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
	{
		var orders = await _context.Orders.Select(o => new OrderDto(o.OrderId, o.OrderDate, o.TotalAmount, o.Status, o.CustomerId)).ToListAsync();

		return Ok(orders);
	}

	[HttpGet("{id:int}", Name = "GetOrderById")]
	public async Task<ActionResult<OrderDto>> GetOrderById(int id)
	{
		var order = await _context.Orders
		.Where(o => o.OrderId == id)
		.Select(o => new OrderDto(o.OrderId, o.OrderDate, o.TotalAmount, o.Status, o.CustomerId))
		.FirstOrDefaultAsync();

		if (order is null)
			return NotFound();


		return Ok(order);
	}

	[HttpGet("/customer/{customerId:int}")]
	public async Task<ActionResult<OrderDto>> GetOrdersForUser(int customerId)
	{
		var orders = await _context.Orders
		.Where(o => o.CustomerId == customerId)
		.Select(o => new OrderDto(o.OrderId, o.OrderDate, o.TotalAmount, o.Status, o.CustomerId))
		.ToListAsync();

		if (orders is null)
			return NotFound();

		return Ok(orders);
	}

	[HttpPost()]
	public async Task<ActionResult<OrderDto>> CreateNewOrder(OrderCreationDto newOrderData)
	{
		var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == newOrderData.CustomerId);

		if (customer is null)
			return BadRequest(new { error = "User not found" });

		var res = await _context.Orders
			.AddAsync(new Order()
			{
				CustomerId = newOrderData.CustomerId,
				OrderDate = DateTime.UtcNow,
				Status = newOrderData.Status,
				TotalAmount = newOrderData.TotalAmount,
			});

		await _context.SaveChangesAsync();

		return CreatedAtAction("GetOrderById",
			new { id = res.Entity.OrderId },
			new OrderDto(res.Entity.OrderId, res.Entity.OrderDate, res.Entity.TotalAmount, res.Entity.Status, res.Entity.CustomerId)
		);
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteOrder(int id)
	{
		var orderToDelete = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

		if (orderToDelete is null)
			return NoContent();


		_context.Orders.Remove(orderToDelete);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> UpdateOrder(int id, OrderUpdateDto orderUpdateData)
	{
		var orderToUpdate = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

		if (orderToUpdate is null)
			return NotFound();

		orderToUpdate.TotalAmount = orderUpdateData.TotalAmount;
		orderToUpdate.Status = orderUpdateData.Status;

		await _context.SaveChangesAsync();
		return NoContent();

	}
}