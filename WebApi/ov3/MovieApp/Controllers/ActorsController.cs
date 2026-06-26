using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos.Actors;
using MovieApp.Enums;
using MovieApp.Services;

namespace MovieApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActorsController(IActorService service) : ControllerBase
{
	private readonly IActorService _service = service;
	private const int maxNumberOfActorsPerPage = 20;

	[HttpGet]
	public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors(
		[FromQuery] string? name,
		[FromQuery] int page = 1,
		[FromQuery] int perPage = 10
	)
	{
		if (perPage > maxNumberOfActorsPerPage)
			perPage = maxNumberOfActorsPerPage;

		var actors = await _service.GetActors(name, page, perPage);

		return Ok(actors);
	}

	[HttpGet("{id:int}", Name = "GetActorById")]
	public async Task<ActionResult<ActorDetailedDto>> GetActorById(int id)
	{
		var actor = await _service.GetActorById(id);

		if (actor is null)
			return NotFound(new { error = "Actor could not be found!" });

		return Ok(actor);
	}

	[HttpPost()]
	public async Task<ActionResult<ActorDto>> CreateActor(ActorCreationDto newActorData)
	{
		var newlyCreatedActor = await _service.CreateActor(newActorData);

		return CreatedAtRoute(
			"GetActorById",
			new { id = newlyCreatedActor.Id },
			new ActorDto(newlyCreatedActor.Id, newlyCreatedActor.Name, newlyCreatedActor.ImageURL, newlyCreatedActor.BirthDate)
		);
	}

	[HttpDelete("{id:int}")]
	public async Task<ActionResult> DeleteActor(int id)
	{
		await _service.DeleteActor(id);
		return NoContent();
	}

	[HttpPut("{id:int}")]
	public async Task<ActionResult> UpdateActor(int id, ActorUpdateDto updateData)
	{
		var result = await _service.UpdateActor(id, updateData);

		return result switch
		{
			UpdateActorResult.ActorNotFound =>
				NotFound(new { error = "No actor found!" }),
			UpdateActorResult.InvalidMovies =>
				BadRequest(new { error = "Not all movies exists!" }),
			UpdateActorResult.Success =>
				NoContent(),
			_ => StatusCode(500)
		};
	}
}