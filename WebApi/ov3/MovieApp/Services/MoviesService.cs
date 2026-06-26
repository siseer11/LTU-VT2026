using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos.Actors;
using MovieApp.Dtos.Movies;
using MovieApp.Dtos.Reviews;
using MovieApp.Dtos.Users;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Results;

namespace MovieApp.Services;

public class MovieService(AppDbContext context) : IMovieService
{
	private AppDbContext _context = context;

	public async Task<PaginatedResult<MovieDto>> GetMovies(string? genre, string? title, string? actor, int page, int itemsPerPage)
	{
		var query = _context.Movies.AsQueryable();

		if (!string.IsNullOrWhiteSpace(genre))
		{
			// movies = movies.Where(m => m.Genre.Name.ToLower().Contains(genre.ToLower()));
			query = query.Where(m => EF.Functions.Like(m.Genre.Name, $"%{genre}%"));
		}

		if (!string.IsNullOrWhiteSpace(title))
		{
			query = query.Where(m => EF.Functions.Like(m.Title, $"%{title}%"));
		}

		if (!string.IsNullOrWhiteSpace(actor))
		{
			query = query.Where(m => m.Actors.Any(a => EF.Functions.Like(a.Name, $"%{actor}%")));
		}

		var totalMoviesInDb = await query.CountAsync();
		var movies = await query
			.Skip((page - 1) * itemsPerPage)
			.Take(itemsPerPage)
			.Select(m => new MovieDto(
				m.Id,
				m.Title,
				m.Year,
				m.ImageURL,
				m.Genre.Name,
				m.Genre.ChildrenSafe
			)).ToListAsync();

		var paginationMetadata = new PaginationMetadata(page, itemsPerPage, totalMoviesInDb);

		return new PaginatedResult<MovieDto>()
		{
			Pagination = paginationMetadata,
			Data = movies
		};
	}

	public async Task<GenericResult<bool, AddActorToMovieCastErrors>> AddActorToMovieCast(int movieId, int actorId)
	{
		//checks
		var movie = await _context.Movies
			.Include(m => m.Actors)
			.FirstOrDefaultAsync(m => m.Id == movieId);
		if (movie is null)
			return new() { Success = false, ErrorCode = AddActorToMovieCastErrors.MovieNotFound };

		var actor = await _context.Actors.FindAsync(actorId);
		if (actor is null)
			return new() { Success = false, ErrorCode = AddActorToMovieCastErrors.ActorNotFound };

		if (movie.Actors.Any(a => a.Id == actorId))
			return new() { Success = false, ErrorCode = AddActorToMovieCastErrors.ActorAlreadyInMovie };

		//add
		movie.Actors.Add(actor);
		await _context.SaveChangesAsync();

		return new() { Success = true };
	}

	public async Task<GenericResult<MovieFullInfoDto, AddMovieErrors>> AddMovie(MovieCreationDto newMovieData)
	{
		// checks
		var genreDoesNotExists = await _context.Genres.FindAsync(newMovieData.GenreId) is null;

		if (genreDoesNotExists)
			return new()
			{
				Success = false,
				ErrorCode = AddMovieErrors.GenreNotFound
			};

		var uniqueIncomingActorIds = newMovieData.Actors.Distinct().ToList();

		var actorsDbData = await _context.Actors
			.Where(a => uniqueIncomingActorIds.Contains(a.Id))
			.ToListAsync();

		if (uniqueIncomingActorIds.Count != actorsDbData.Count)
			return new()
			{
				Success = false,
				ErrorCode = AddMovieErrors.NotAllActorsExist
			};

		// add
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

		var newMovieDb = res.Entity;

		return new()
		{
			Success = true,
			Data = new MovieFullInfoDto(
				newMovieDb.Id,
				newMovieDb.Title,
				newMovieDb.Year,
				newMovieDb.ImageURL,
				newMovieDb.Genre.Name,
				newMovieDb.Genre.ChildrenSafe,
				newMovieDb.MovieDetails.Synopsis,
				newMovieDb.MovieDetails.Language,
				newMovieDb.MovieDetails.Budget,
				[.. newMovieDb.Actors.Select(a => new ActorDto(a.Id, a.Name, a.ImageURL, a.BirthDate))],
				[]
			)
		};
	}

	public async Task DeleteMovie(int id)
	{
		await _context.Movies
			.Where(m => m.Id == id)
			.ExecuteDeleteAsync();
	}

	public async Task<GenericResult<bool, FullyUpdateMovieErrors>> FullyUpdateMovie(int id, MovieUpdateDto updateData)
	{
		// checks
		var movie = await _context.Movies
			.Include(m => m.Actors)
			.Include(m => m.MovieDetails)
			.FirstOrDefaultAsync(m => m.Id == id);
		if (movie is null)
			return new() { ErrorCode = FullyUpdateMovieErrors.MovieNotFound, Success = false };

		var genre = await _context.Genres.FindAsync(updateData.GenreId);
		if (genre is null)
			return new() { ErrorCode = FullyUpdateMovieErrors.GenreNotFound, Success = false };

		var uniqueIncActors = updateData.Actors.Distinct().ToList();
		var actorsFromDb = await _context.Actors.Where(a => uniqueIncActors.Contains(a.Id)).ToListAsync();
		if (uniqueIncActors.Count != actorsFromDb.Count)
			return new() { ErrorCode = FullyUpdateMovieErrors.NotAllActorsExist, Success = false };


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

		return new() { Success = true };
	}

	public async Task<MovieFullInfoDto?> GetMovieById(int id)
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

		return movie;
	}

}