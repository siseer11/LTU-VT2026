using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApiFirst.Models;

namespace WebApiFirst.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
	private static readonly List<CarDto> Cars = [
		new CarDto(1, "Honda Civic", "red", new DateOnly(2022,11,21)),
		new CarDto(2, "Honda Civic 2", "blue", new DateOnly(2022,11,21)),
		new CarDto(3, "Honda Civic 3", "huait", new DateOnly(2022,11,21)),
	];

	[HttpGet()]
	public ActionResult<IEnumerable<CarDto>> GetCars()
	{
		return Ok(Cars);
	}

	[HttpGet("{id:int}", Name = "GetCarById")]
	public ActionResult<CarDto> GetCarById(int id)
	{
		var wantedCar = Cars.FirstOrDefault(car => car.Id == id);

		if (wantedCar is null)
			return NotFound();

		return Ok(wantedCar);
	}

	[HttpPost]
	public ActionResult<CarDto> CreateNewCar([FromBody] CarCreationDto newCarData)
	{
		int maxCarsId = Cars.Max(c => c.Id);
		int newCarId = maxCarsId + 1;

		var newCar = new CarDto(newCarId, newCarData.Name, newCarData.Color, newCarData.FabricationDate);

		Cars.Add(newCar);

		return CreatedAtRoute("GetCarById", new { id = newCarId }, newCar); // we pass down the params for the route, if the route would have multiple params, each one of them needs to be passed
	}

}
