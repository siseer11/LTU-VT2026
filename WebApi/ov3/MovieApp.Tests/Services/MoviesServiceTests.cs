using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos.Movies;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Tests.Helpers;

namespace MovieApp.Tests.Services;

public class MovieServicesTests
{
	private readonly AppDbContext _context;
	private readonly MovieService _service;

	public MovieServicesTests()
	{
		_context = TestDbContextFactory.Create();
		_service = new MovieService(_context);
	}

	private async Task<Genre> CreateGenre()
	{
		var genre = new Genre() { Name = "Horror", ChildrenSafe = true };
		await _context.Genres.AddAsync(genre);
		await _context.SaveChangesAsync();

		return genre;
	}

	private async Task<Actor> CreateActor(string name = "Hector B")
	{
		var actor = new Actor() { Name = name, ImageURL = "test.jpg", BirthDate = new DateOnly() };
		await _context.Actors.AddAsync(actor);
		await _context.SaveChangesAsync();

		return actor;
	}

	private async Task<Movie> CreateMovie(Genre? genre, List<Actor>? actors, bool skipDbSave = false, string Title = "World of Cold")
	{
		Movie movie = new()
		{
			Title = Title,
			ImageURL = "movieCover.jpg",
			Year = 1992,
		};

		if (genre is not null)
		{
			movie.Genre = genre;
		}

		if (actors is not null)
		{
			movie.Actors = actors;
		}

		if (!skipDbSave)
		{
			await _context.Movies.AddAsync(movie);
			await _context.SaveChangesAsync();
		}

		return movie;
	}

	[Theory]
	[InlineData(null, null, null, 3)]
	[InlineData("Action", null, null, 0)]
	[InlineData("Horror", null, null, 3)]
	[InlineData("Horror", "World", null, 2)]
	[InlineData("Horror", "World", "Hector", 2)]
	[InlineData(null, null, "Hector", 2)]
	public async Task GetMovies_ReturnsCorrectList_WithDifferentParams(string? genreParam, string? titleParam, string? actorParam, int expectedNumberOfItems)
	{
		// Arrange
		var genre = await CreateGenre();
		var actor = await CreateActor();

		Movie movie1 = await CreateMovie(genre, [actor], true, "World of Cold");
		Movie movie2 = await CreateMovie(genre, null, true, "Fast and Slow");
		Movie movie3 = await CreateMovie(genre, [actor], true, "World of Bikecraft");

		List<MovieDetails> movieDetails = [
			new(){Budget = 0, Language = "Ro", Synopsis = "Best", Movie = movie1},
			new(){Budget = 1, Language = "En", Synopsis = "Best", Movie = movie2},
			new(){Budget = 999, Language = "En", Synopsis = "Worst", Movie = movie3}
		];
		await _context.MovieDetails.AddRangeAsync(movieDetails);

		movie1.MovieDetails = movieDetails[0];
		movie2.MovieDetails = movieDetails[1];
		movie3.MovieDetails = movieDetails[2];

		List<Movie> movies = [
			movie1,
			movie2,
			movie3
		];

		await _context.Movies.AddRangeAsync(movies);
		await _context.SaveChangesAsync();

		// Act
		var response = await _service.GetMovies(genreParam, titleParam, actorParam, 1, 10);


		// Assert
		Assert.Equal(expectedNumberOfItems, response.Pagination.TotalItemsCount);
	}

	[Fact]
	public async Task AddActorToMovieCast_ReturnsErrorCode_WhenMovieDoesNotExist()
	{
		// Arrange
		// Act
		var response = await _service.AddActorToMovieCast(1, 1);

		// Assert
		Assert.False(response.Success);
		Assert.Equal(AddActorToMovieCastErrors.MovieNotFound, response.ErrorCode);
	}

	[Fact]
	public async Task AddActorToMovieCast_ReturnsErrorCode_WhenActorDoesNotExist()
	{
		// Arrange
		var genre = await CreateGenre();
		Movie movie = await CreateMovie(genre, null);
		// Act
		var response = await _service.AddActorToMovieCast(movie.Id, 1);

		// Assert
		Assert.False(response.Success);
		Assert.Equal(AddActorToMovieCastErrors.ActorNotFound, response.ErrorCode);
	}

	[Fact]
	public async Task AddActorToMovieCast_ReturnsErrorCode_WhenActorAlreadyInMovie()
	{
		// Arrange
		var genre = await CreateGenre();
		var actor = await CreateActor();
		Movie movie = await CreateMovie(genre, [actor]);
		// Act
		var response = await _service.AddActorToMovieCast(movie.Id, actor.Id);

		// Assert
		Assert.False(response.Success);
		Assert.Equal(AddActorToMovieCastErrors.ActorAlreadyInMovie, response.ErrorCode);
	}

	[Fact]
	public async Task AddActorToMovieCast_ReturnsSuccessCode_WhenDataIsCorrect()
	{
		// Arrange
		var genre = await CreateGenre();
		var oldActor = await CreateActor();
		var newActor = await CreateActor("Boby B");
		Movie movie = await CreateMovie(genre, [oldActor]);

		// Act
		var response = await _service.AddActorToMovieCast(movie.Id, newActor.Id);

		// Assert
		Assert.True(response.Success);

		var movieActorsDb = await _context.Movies.Include(m => m.Actors).FirstOrDefaultAsync(m => m.Id == movie.Id);

		Assert.Equal(2, movieActorsDb!.Actors.Count);
		Assert.Contains(movieActorsDb!.Actors, a => a.Name == "Boby B");
		Assert.Contains(movieActorsDb!.Actors, a => a.Id == newActor.Id);
	}


	private static MovieCreationDto GenerateMovieCreationDto(List<int>? Actors, int? GenreId)
	{
		return new()
		{
			Title = "Amazing Movie 2",
			Actors = Actors is null ? [1, 2] : Actors,
			ImageURL = "Aa.jpg",
			Language = "Ro",
			Synopsis = "One of the worst, must watch",
			Budget = 0,
			GenreId = GenreId ?? 1,
			Year = 1993
		};
	}

	[Fact]
	public async Task AddMovie_ReturnsCorrectErrorCode_WhenGenreDoesNotExist()
	{
		// Arrange
		var movieToBeAdded = GenerateMovieCreationDto(null, null);
		// Act
		var response = await _service.AddMovie(movieToBeAdded);

		// Assert
		Assert.False(response.Success);
		Assert.Equal(AddMovieErrors.GenreNotFound, response.ErrorCode);
	}

	[Fact]
	public async Task AddMovie_ReturnsCorrectErrorCode_WhenNotAllActorsExist()
	{
		// Arrange
		var actor = await CreateActor();
		var genre = await CreateGenre();

		await _context.SaveChangesAsync();

		var movieToBeAdded = GenerateMovieCreationDto([actor.Id, 9999], genre.Id);
		// Act
		var response = await _service.AddMovie(movieToBeAdded);

		// Assert
		Assert.False(response.Success);
		Assert.Equal(AddMovieErrors.NotAllActorsExist, response.ErrorCode);
	}

	[Fact]
	public async Task AddMovie_ReturnsSuccessWithNewObject_WhenAllDataIsCorrect()
	{
		// Arrange
		var actor = await CreateActor();
		var genre = await CreateGenre();

		var movieToBeAdded = GenerateMovieCreationDto([actor.Id], genre.Id);
		// Act
		var response = await _service.AddMovie(movieToBeAdded);

		// Assert
		Assert.True(response.Success);
		Assert.IsType<MovieFullInfoDto>(response.Data);

		var movies = await _context.Movies.ToListAsync();
		Assert.Contains(movies, m => m.Title == "Amazing Movie 2");
		Assert.Contains(movies, m => m.Id == response.Data!.Id);
	}

	[Fact]
	public async Task AddMovie_IgnoresDuplicateActorIds()
	{
		// Arrange
		var actor = await CreateActor();
		var genre = await CreateGenre();

		var movieToBeAdded = GenerateMovieCreationDto([actor.Id, actor.Id, actor.Id, actor.Id, actor.Id], genre.Id);

		// Act
		var response = await _service.AddMovie(movieToBeAdded);

		// Assert
		Assert.True(response.Success);

		var movieInDb = await _context.Movies
			.Include(m => m.Actors)
			.FirstOrDefaultAsync(m => m.Id == response.Data!.Id);
		Assert.Single(movieInDb!.Actors);
	}

	[Fact]
	public async Task DeleteMovie_RemovesMovieFromDb()
	{
		// Arrange
		var actor = await CreateActor();
		var genre = await CreateGenre();
		var movieToBeAdded = await CreateMovie(genre, [actor]);

		// Act
		await _service.DeleteMovie(movieToBeAdded.Id);

		// Assert
		var response = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieToBeAdded.Id);

		Assert.Null(response);
	}

	private static MovieUpdateDto GenerateMovieUpdateDto(int? genreId, List<int>? actors)
	{
		return new()
		{
			Actors = actors ?? [1],
			ImageURL = "t.jpg",
			Language = "En",
			Synopsis = "Some",
			Title = "Title Test",
			Budget = 0,
			GenreId = genreId ?? 1,
			Year = 1899
		};
	}

	[Fact]
	public async Task FullyUpdateMovie_ReturnsCorrectErrorCode_WhenMovieNotFound()
	{
		// Arrange

		// Act
		var response = await _service.FullyUpdateMovie(1, GenerateMovieUpdateDto(null, null));
		// Assert

		Assert.False(response.Success);
		Assert.Equal(FullyUpdateMovieErrors.MovieNotFound, response.ErrorCode);
	}

	[Fact]
	public async Task FullyUpdateMovie_ReturnsCorrectErrorCode_WhenGenreDoesNotExist()
	{
		// Arrange
		var actor = await CreateActor();
		var genre = await CreateGenre();
		var movieToBeAdded = await CreateMovie(genre, [actor]);

		// Act
		var response = await _service.FullyUpdateMovie(movieToBeAdded.Id, GenerateMovieUpdateDto(9999, null));
		// Assert

		Assert.False(response.Success);
		Assert.Equal(FullyUpdateMovieErrors.GenreNotFound, response.ErrorCode);
	}

	[Fact]
	public async Task FullyUpdateMovie_ReturnsCorrectErrorCode_WhenNotAllActorsExist()
	{
		// Arrange
		var actor = await CreateActor();
		var genre = await CreateGenre();
		var movieToBeAdded = await CreateMovie(genre, [actor]);

		// Act
		var response = await _service.FullyUpdateMovie(movieToBeAdded.Id, GenerateMovieUpdateDto(genre.Id, [actor.Id, 9999]));
		// Assert

		Assert.False(response.Success);
		Assert.Equal(FullyUpdateMovieErrors.NotAllActorsExist, response.ErrorCode);
	}

	[Fact]
	public async Task FullyUpdateMovie_ReturnsSuccess_WhenMovieUpdatedInDb()
	{
		// Arrange
		var actor = await CreateActor();
		var genre = await CreateGenre();
		var movieToBeAdded = await CreateMovie(genre, [actor], true);

		MovieDetails movieDetails = new() { Budget = 999, Language = "En", Synopsis = "Worst", Movie = movieToBeAdded };
		await _context.MovieDetails.AddAsync(movieDetails);

		movieToBeAdded.MovieDetails = movieDetails;
		await _context.SaveChangesAsync();

		// Act
		var response = await _service.FullyUpdateMovie(movieToBeAdded.Id, GenerateMovieUpdateDto(genre.Id, [actor.Id]));

		// Assert
		Assert.True(response.Success);

		var updatedMovieFromDb = await _context.Movies.Include(m => m.MovieDetails).FirstOrDefaultAsync(m => m.Id == movieToBeAdded.Id);

		Assert.Equal("t.jpg", updatedMovieFromDb!.ImageURL);
		Assert.Equal("Some", updatedMovieFromDb!.MovieDetails.Synopsis);
		Assert.Equal("Title Test", updatedMovieFromDb!.Title);
		Assert.Equal(1899, updatedMovieFromDb!.Year);
	}

	[Fact]
	public async Task GetMovieById_ReturnsNull_IfNoMovieFound()
	{
		// Arrange

		// Act
		var response = await _service.GetMovieById(1);
		// Assert
		Assert.Null(response);
	}

	[Fact]
	public async Task GetMovieById_ReturnsCorrectMovie()
	{
		// Arrange
		var actor = await CreateActor();
		var genre = await CreateGenre();
		var movieToBeAdded = await CreateMovie(genre, [actor], true);

		MovieDetails movieDetails = new() { Budget = 0, Language = "Ro", Synopsis = "Best", Movie = movieToBeAdded };
		await _context.MovieDetails.AddRangeAsync(movieDetails);

		movieToBeAdded.MovieDetails = movieDetails;

		await _context.Movies.AddAsync(movieToBeAdded);
		await _context.SaveChangesAsync();
		// Act
		var response = await _service.GetMovieById(movieToBeAdded.Id);

		// Assert
		Assert.NotNull(response);
		Assert.IsType<MovieFullInfoDto>(response);
		Assert.Equal(movieToBeAdded.Id, response.Id);
		Assert.Equal(movieToBeAdded.Title, response.Title);
		Assert.Equal(movieToBeAdded.Year, response.Year);
		Assert.Equal(movieToBeAdded.ImageURL, response.ImageURL);
		Assert.Equal(movieToBeAdded.Genre.ChildrenSafe, response.ChildrenSafe);
		Assert.Equal(movieToBeAdded.MovieDetails.Synopsis, response.Synopsis);
		Assert.Equal(movieToBeAdded.MovieDetails.Budget, response.Budget);
	}
}
