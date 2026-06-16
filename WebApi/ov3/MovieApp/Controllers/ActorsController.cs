using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos.Actors;
using MovieApp.Dtos.Movies;
using MovieApp.Models;

namespace MovieApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActorsController(AppDbContext context) : ControllerBase
{
	private readonly AppDbContext _context = context;

	[HttpGet]
	public async Task<ActionResult<ActorDto>> GetActors()
	{
		var actors = await _context.Actors
			.Select(a => new ActorDto(a.Id, a.Name, a.ImageURL, a.BirthDate))
			.ToListAsync();

		return Ok(actors);
	}

	[HttpGet("{id:int}", Name = "GetActorById")]
	public async Task<ActionResult<ActorDetailedDto>> GetActorById(int id)
	{
		var actor = await _context.Actors
			.Where(a => a.Id == id)
			.Select(a => new ActorDetailedDto(
				a.Id,
				a.Name,
				a.ImageURL,
				a.BirthDate,
				a.Movies.Select(m => new MovieDto(
					m.Id,
					m.Title,
					m.Year,
					m.ImageURL,
					m.Genre.Name,
					m.Genre.ChildrenSafe
				)).ToList()
			)).FirstOrDefaultAsync();

		if (actor is null)
			return NotFound(new { error = "Actor could not be found!" });

		return Ok(actor);
	}

	[HttpPost()]
	public async Task<ActionResult<ActorDto>> CreateActor(ActorCreationDto newActorData)
	{
		var res = await _context.Actors.AddAsync(new Actor()
		{
			ImageURL = newActorData.ImageURL,
			Name = newActorData.Name,
			BirthDate = newActorData.BirthDate
		});

		await _context.SaveChangesAsync();

		var actorDb = res.Entity;
		return CreatedAtRoute(
			"GetActorById",
			new { id = actorDb.Id },
			new ActorDto(actorDb.Id, actorDb.Name, actorDb.ImageURL, actorDb.BirthDate)
		);
	}

	[HttpDelete("{id:int}")]
	public async Task<ActionResult> DeleteActor(int id)
	{
		await _context.Actors
			.Where(a => a.Id == id)
			.ExecuteDeleteAsync();

		return NoContent();
	}

	[HttpPut("{id:int}")]
	public async Task<ActionResult> UpdateActor(int id, ActorUpdateDto updateData)
	{
		//checks
		var actor = await _context.Actors
			.Include(a => a.Movies)
			.FirstOrDefaultAsync(a => a.Id == id);

		if (actor is null)
			return NotFound(new { error = "No actor found!" });

		var uniqueUpdateMovies = updateData.Movies.Distinct().ToList();
		var moviesFromDb = await _context.Movies
			.Where(m => uniqueUpdateMovies.Contains(m.Id))
			.ToListAsync();
		if (uniqueUpdateMovies.Count != moviesFromDb.Count)
			return BadRequest(new { error = "Not all movies exist!" });

		//update data

		actor.Movies.Clear();
		foreach (Movie movie in moviesFromDb)
		{
			actor.Movies.Add(movie);
		}

		actor.BirthDate = updateData.BirthDate;
		actor.Name = updateData.Name;
		actor.ImageURL = updateData.ImageURL;

		await _context.SaveChangesAsync();
		return NoContent();
	}

}