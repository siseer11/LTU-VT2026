using Microsoft.AspNetCore.Mvc;

namespace MixedApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
	readonly List<Car> cars = [
		new Car(1, "Test Brand", "Test Model"),
		new Car(2, "Test Brand 2", "Test Model"),
	];

	[HttpGet]
	public ActionResult<IEnumerable<Car>> GetListOfCars()
	{

		return Ok(cars);
	}
}

public record Car(int Id, string Brand, string Model);