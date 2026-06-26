using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos.Movies;
using MovieApp.Services;
using MovieApp.Enums;

namespace MovieApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(IMovieService service) : ControllerBase
{
	private readonly IMovieService _service = service;
	private const int maxMoviesPerPage = 20;

	[HttpGet]
	public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies(
		[FromQuery] string? genre,
		[FromQuery] string? title,
		[FromQuery] string? actor,
		[FromQuery] int page = 1,
		[FromQuery] int perPage = 10
	)
	{
		if (perPage > maxMoviesPerPage)
			perPage = maxMoviesPerPage;

		var movies = await _service.GetMovies(genre, title, actor, page, perPage);

		return Ok(movies);
	}

	[HttpGet("{id:int}", Name = "GetMovieById")]
	public async Task<ActionResult<MovieFullInfoDto>> GetMovieById(int id)
	{
		var movie = await _service.GetMovieById(id);

		if (movie is null)
			return NotFound();

		return Ok(movie);
	}

	[HttpPost]
	public async Task<ActionResult<MovieFullInfoDto>> AddMovie(MovieCreationDto newMovieData)
	{
		var result = await _service.AddMovie(newMovieData);

		if (result.Success == false)
		{
			return result.ErrorCode switch
			{
				AddMovieErrors.GenreNotFound =>
					BadRequest(new { error = "Genre does not exist!" }),
				AddMovieErrors.NotAllActorsExist =>
					BadRequest(new { error = "Not all actors exist!" }),
				_ => StatusCode(500)
			};
		}

		return CreatedAtRoute("GetMovieById", result.Data!.Id, result.Data);
	}


	[HttpDelete("{id:int}")]
	public async Task<ActionResult> DeleteMovie(int id)
	{
		await _service.DeleteMovie(id);

		return NoContent();
	}

	[HttpPut("{id:int}")]
	public async Task<ActionResult> FullyUpdateMovie(int id, MovieUpdateDto updateData)
	{
		var result = await _service.FullyUpdateMovie(id, updateData);

		if (result.Success == false)
		{
			return result.ErrorCode switch
			{
				FullyUpdateMovieErrors.GenreNotFound =>
					BadRequest(new { error = "Genre does not exist!" }),
				FullyUpdateMovieErrors.NotAllActorsExist =>
					BadRequest(new { error = "Not all actors exist!" }),
				FullyUpdateMovieErrors.MovieNotFound =>
					NotFound(),
				_ => StatusCode(500)
			};
		}

		return NoContent();
	}


	[HttpPost("{movieId:int}/actors/{actorId:int}")]
	public async Task<ActionResult> AddActorToMovieCast(int movieId, int actorId)
	{
		var result = await _service.AddActorToMovieCast(movieId, actorId);

		if (result.Success == false)
		{
			return result.ErrorCode switch
			{
				AddActorToMovieCastErrors.MovieNotFound =>
					NotFound(new { error = "Movie not found!" }),
				AddActorToMovieCastErrors.ActorNotFound =>
					NotFound(new { error = "Actor not found!" }),
				AddActorToMovieCastErrors.ActorAlreadyInMovie =>
					BadRequest(new { error = "Actor already in the movie!" }),
				_ => StatusCode(500)
			};
		}

		return NoContent();
	}
}
