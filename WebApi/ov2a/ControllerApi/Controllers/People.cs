using ControllerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControllerApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class People : ControllerBase
{

	readonly List<Person> PeopleList = [
		new Person(1, "Test 1", 51),
		new Person(2, "Test 2", 5),
		new Person(3, "Test 3", 112),
	];

	[HttpGet]
	public ActionResult<IEnumerable<Person>> GetPeopleList()
	{
		return Ok(PeopleList);
	}

	[HttpGet("{id:int}")]
	public ActionResult<Person> GetPersonById(int id)
	{
		var wantedPerson = PeopleList.FirstOrDefault(p => p.Id == id);

		if (wantedPerson is null)
			return NotFound();

		return Ok(wantedPerson);
	}

}
