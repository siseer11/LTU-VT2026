using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;
using MovieApp.Dtos.Movies;
using MovieApp.Dtos.Actors;
using MovieApp.Dtos.Reviews;
using MovieApp.Dtos.Users;

namespace MovieApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(AppDbContext context) : ControllerBase
{
	private readonly AppDbContext _context = context;

	[HttpGet]
	public async Task<ActionResult<MovieDto>> GetMovies()
	{
		var movies = await _context.Movies
			.Select(m => new MovieDto(
				m.Id,
				m.Title,
				m.Year,
				m.ImageURL,
				m.Genre.Name,
				m.Genre.ChildrenSafe
			)).ToListAsync();

		return Ok(movies);
	}

	[HttpGet("{id:int}", Name = "GetMovieById")]
	public async Task<ActionResult<MovieFullInfoDto>> GetMovieById(int id)
	{
		var movie = await _context.Movies
			.Where(m => m.Id == id)
			.Select(m => new MovieFullInfoDto(
				m.Id,
				m.Title,
				m.Year,
				m.ImageURL,
				m.Genre.Name,
				m.Genre.ChildrenSafe,
				m.MovieDetails.Synopsis,
				m.MovieDetails.Language,
				m.MovieDetails.Budget,
				m.Actors.Select(
					a => new ActorDto(a.Id, a.Name, a.ImageURL, a.BirthDate)
				).ToList(),
				m.Reviews.Select(r =>
					new ReviewDto(
						r.Id,
						r.Rating,
						r.Comment,
						r.Edited,
						r.EditedAt,
						r.CreatedAt,
						new UserDto(
							r.Reviewer.Id,
							r.Reviewer.Name,
							r.Reviewer.ImageURL,
							r.Reviewer.IsAHater
						))
				).ToList()
			)).FirstOrDefaultAsync();

		if (movie is null)
			return NotFound();

		return Ok(movie);
	}

	[HttpGet("{id:int}/details")]
	public async Task<ActionResult<MovieDetailsDto>> GetMovieDetailsByMovieId(int id)
	{
		//checks
		var movie = await _context.Movies
			.Include(m => m.MovieDetails)
			.FirstOrDefaultAsync(m => m.Id == id);

		if (movie is null)
			return NotFound(new { error = "No movie can be found!" });

		var movieDetails = movie.MovieDetails;

		return Ok(new MovieDetailsDto(movieDetails.Id, movieDetails.Synopsis, movieDetails.Language, movieDetails.Budget));
	}

	[HttpPost]
	public async Task<ActionResult<MovieFullInfoDto>> AddMovie(MovieCreationDto newMovieData)
	{
		// checks
		var genreDoesNotExists = await _context.Genres.FindAsync(newMovieData.GenreId) is null;

		if (genreDoesNotExists)
			return BadRequest(new { error = "Genre does not exist!" });

		var uniqueIncomingActorIds = newMovieData.Actors.Distinct().ToList();

		var actorsDbData = await _context.Actors
			.Where(a => uniqueIncomingActorIds.Contains(a.Id))
			.ToListAsync();

		if (uniqueIncomingActorIds.Count != actorsDbData.Count)
			return BadRequest(new { error = "Not all actors exist!" });

		Movie movie = new()
		{
			Title = newMovieData.Title,
			ImageURL = newMovieData.ImageURL,
			GenreId = newMovieData.GenreId,
			Year = newMovieData.Year,
			Actors = actorsDbData,
			MovieDetails = new MovieDetails()
			{
				Language = newMovieData.Language,
				Synopsis = newMovieData.Synopsis,
				Budget = newMovieData.Budget,
			}
		};

		var res = await _context.Movies.AddAsync(movie);
		await _context.SaveChangesAsync();

		var newMovie = res.Entity;

		return CreatedAtRoute(
			"GetMovieById",
			new { id = newMovie.Id },
			new MovieFullInfoDto(
				newMovie.Id,
				newMovie.Title,
				newMovie.Year,
				newMovie.ImageURL,
				newMovie.Genre.Name,
				newMovie.Genre.ChildrenSafe,
				newMovie.MovieDetails.Synopsis,
				newMovie.MovieDetails.Language,
				newMovie.MovieDetails.Budget,
				[.. newMovie.Actors.Select(a => new ActorDto(a.Id, a.Name, a.ImageURL, a.BirthDate))],
				[]
				)
		);
	}

	[HttpPut("{id:int}")]
	public async Task<ActionResult> FullyUpdateMovie(int id, MovieUpdateDto updateData)
	{
		// checks
		var movie = await _context.Movies
			.Include(m => m.Actors)
			.Include(m => m.MovieDetails)
			.FirstOrDefaultAsync(m => m.Id == id);
		if (movie is null)
			return NotFound(new { error = "The movie can not be found!" });

		var genre = await _context.Genres.FindAsync(updateData.GenreId);
		if (genre is null)
			return BadRequest(new { error = "Genre not supported!" });

		var uniqueIncActors = updateData.Actors.Distinct().ToList();
		var actorsFromDb = await _context.Actors.Where(a => uniqueIncActors.Contains(a.Id)).ToListAsync();
		if (uniqueIncActors.Count != actorsFromDb.Count)
			return BadRequest(new { error = "Genre not supported!" });


		// Update
		movie.Actors.Clear();
		foreach (Actor actor in actorsFromDb)
		{
			movie.Actors.Add(actor);
		}

		movie.Title = updateData.Title;
		movie.Year = updateData.Year;
		movie.ImageURL = updateData.ImageURL;
		movie.GenreId = updateData.GenreId;
		movie.MovieDetails.Budget = updateData.Budget;
		movie.MovieDetails.Language = updateData.Language;
		movie.MovieDetails.Synopsis = updateData.Synopsis;

		await _context.SaveChangesAsync();

		return NoContent();
	}

	[HttpDelete("{id:int}")]
	public async Task<ActionResult> DeleteMovie(int id)
	{
		await _context.Movies
			.Where(m => m.Id == id)
			.ExecuteDeleteAsync();

		return NoContent();
	}

	[HttpPost("{movieId:int}/actors/{actorId:int}")]
	public async Task<ActionResult> AddActorToMovieCast(int movieId, int actorId)
	{
		//checks
		var movie = await _context.Movies
			.Include(m => m.Actors)
			.FirstOrDefaultAsync(m => m.Id == movieId);
		if (movie is null)
			return NotFound(new { error = "Movie not found!" });

		var actor = await _context.Actors.FindAsync(actorId);
		if (actor is null)
			return NotFound(new { error = "Actor not found!" });

		if (movie.Actors.Any(a => a.Id == actorId))
			return BadRequest(new { error = "Actor already in the movie!" });

		//add
		movie.Actors.Add(actor);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
