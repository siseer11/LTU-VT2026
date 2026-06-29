using Microsoft.EntityFrameworkCore;
using MovieApp.Dtos.Actors;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Results;
using MovieApp.Services;
using MovieApp.Tests.Helpers;

namespace MovieApp.Tests.Services;

public class ActorServiceTests
{
	[Fact]
	public async Task GetActorById_ReturnsActor_WhenActorExists()
	{
		// Arrage
		var context = TestDbContextFactory.Create();

		Actor actor = new() { Name = "Test", ImageURL = "Test.jpg", BirthDate = new DateOnly() };

		context.Actors.Add(actor);

		await context.SaveChangesAsync();

		var service = new ActorService(context);

		// Act
		var result = await service.GetActorById(actor.Id);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(actor.Name, result.Name);
		Assert.Equal(actor.ImageURL, result.ImageURL);
		Assert.Equal(actor.BirthDate, result.BirthDate);
	}

	[Fact]
	public async Task GetActorById_ReturnsNull_WhenActorDoesNotExist()
	{
		// Arrage
		var context = TestDbContextFactory.Create();

		var service = new ActorService(context);

		// Act
		var result = await service.GetActorById(1);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public async Task GetActors_ReturnsAllActors_WhenNameIsNull()
	{
		// Arrange
		var context = TestDbContextFactory.Create();
		List<Actor> actors = [
			new(){ Name = "Test", ImageURL = "Test.jpg", BirthDate = new DateOnly() },
			new(){ Name = "Test 2", ImageURL = "Test2.jpg", BirthDate = new DateOnly() },
			new(){ Name = "Test 3", ImageURL = "Test3.jpg", BirthDate = new DateOnly() },
			new(){ Name = "Test 4", ImageURL = "Test4.jpg", BirthDate = new DateOnly() }
		];

		context.AddRange(actors);

		await context.SaveChangesAsync();

		var service = new ActorService(context);

		// Act
		var result = await service.GetActors(null, 1, 10);

		// Assert
		Assert.NotEmpty(result.Data);
		Assert.Equal(actors.Count, result.Pagination.TotalItemsCount);
	}

	[Fact]
	public async Task GetActors_ReturnsFilteredActors_WhenNameProvided()
	{
		// Arrange
		var context = TestDbContextFactory.Create();
		List<Actor> actors = [
			new(){ Name = "John Doe", ImageURL = "Test.jpg", BirthDate = new DateOnly() },
			new(){ Name = "Billy B", ImageURL = "Test2.jpg", BirthDate = new DateOnly() },
			new(){ Name = "Teddy T", ImageURL = "Test3.jpg", BirthDate = new DateOnly() },
			new(){ Name = "John I", ImageURL = "Test4.jpg", BirthDate = new DateOnly() },
			new(){ Name = "Teddy Tohn", ImageURL = "Test5.jpg", BirthDate = new DateOnly() },
		];

		context.AddRange(actors);

		await context.SaveChangesAsync();

		var service = new ActorService(context);

		// Act
		var results = await service.GetActors("ohn", 1, 10);

		// Assert
		Assert.NotEmpty(results.Data);
		Assert.Equal(3, results.Pagination.TotalItemsCount);
		Assert.Contains(results.Data, actor => actor.Name == "John Doe");
		Assert.Contains(results.Data, actor => actor.Name == "John I");
		Assert.Contains(results.Data, actor => actor.Name == "Teddy Tohn");
	}

	[Fact]
	public async Task GetActors_ReturnsEmptyCollection_WhenNoMatchesFound()
	{
		// Arrange
		var context = TestDbContextFactory.Create();
		List<Actor> actors = [
			new(){ Name = "John Doe", ImageURL = "Test.jpg", BirthDate = new DateOnly() },
			new(){ Name = "Billy B", ImageURL = "Test2.jpg", BirthDate = new DateOnly() },
			new(){ Name = "Teddy T", ImageURL = "Test3.jpg", BirthDate = new DateOnly() },
			new(){ Name = "John I", ImageURL = "Test4.jpg", BirthDate = new DateOnly() },
			new(){ Name = "Teddy Tohn", ImageURL = "Test5.jpg", BirthDate = new DateOnly() },
		];

		context.AddRange(actors);

		await context.SaveChangesAsync();

		var service = new ActorService(context);

		// Act
		var results = await service.GetActors("xoz", 1, 10);

		// Assert
		Assert.Empty(results.Data);
	}

	[Fact]
	public async Task CreateActor_AddsActorToDatabase()
	{
		// Arrange
		var context = TestDbContextFactory.Create();
		var service = new ActorService(context);

		// Act
		var res = await service.CreateActor(new ActorCreationDto("Test", "test.jpg", new DateOnly()));

		// Asert
		Assert.NotNull(res);
		Assert.IsType<Actor>(res);
		Assert.Equal("Test", res.Name);
		Assert.Equal("test.jpg", res.ImageURL);
		Assert.Empty(res.Movies);
	}

	[Fact]
	public async Task DeleteActor_RemovesActorFromDatabase()
	{
		// Arrange
		var context = TestDbContextFactory.Create();
		Actor actor = new() { Name = "Test Delete", ImageURL = "TestDelete.jpg", BirthDate = new DateOnly() };

		context.Actors.Add(actor);
		await context.SaveChangesAsync();

		var service = new ActorService(context);

		// Act
		await service.DeleteActor(actor.Id);
		context.ChangeTracker.Clear();

		// Assert
		var actorFromDb = await context.Actors.FindAsync(actor.Id);

		Assert.Null(actorFromDb);
	}

	[Fact]
	public async Task UpdateActor_ReturnsActorNotFound_WhenActorMissing()
	{
		// Arrange
		var context = TestDbContextFactory.Create();
		var service = new ActorService(context);

		// Act
		var result = await service
			.UpdateActor(1, new ActorUpdateDto("Test 99", "Test99.jpg", new DateOnly(), []));

		// Assert
		Assert.Equal(UpdateActorResult.ActorNotFound, result);
	}

	[Fact]
	public async Task UpdateActor_ReturnsInvalidMovies_WhenMoviesDoNotExist()
	{
		// Arrange
		var context = TestDbContextFactory.Create();

		Actor actor = new() { Name = "Test", ImageURL = "Test.jpg", BirthDate = new DateOnly() };
		context.Add(actor);

		await context.SaveChangesAsync();

		var service = new ActorService(context);

		// Act
		var result = await service
			.UpdateActor(actor.Id, new ActorUpdateDto("Test 99", "Test99.jpg", new DateOnly(), [1, 2, 3]));

		// Assert
		Assert.Equal(UpdateActorResult.InvalidMovies, result);
	}

	[Fact]
	public async Task UpdateActor_ReturnsSuccess_WhenValidDataProvided()
	{
		// Arrange
		var context = TestDbContextFactory.Create();

		Genre genre = new() { ChildrenSafe = true, Name = "Test genre" };
		context.Genres.Add(genre);

		List<Movie> movies = [
			new(){Title = "Movie test", ImageURL = "MovieTest.jpg", Genre = genre, Year = 2025},
			new(){Title = "Movie test 2", ImageURL = "MovieTest2.jpg", Genre = genre, Year = 1995},
			new(){Title = "Movie test 3", ImageURL = "MovieTest3.jpg", Genre = genre, Year = 1955}
		];
		context.Movies.AddRange(movies);

		Actor actor = new() { Name = "Test", ImageURL = "Test.jpg", BirthDate = new DateOnly() };
		context.Add(actor);
		await context.SaveChangesAsync();

		var service = new ActorService(context);

		// Act
		var result = await service
			.UpdateActor(actor.Id, new ActorUpdateDto("Test 99", "Test99.jpg", new DateOnly(), [movies[0].Id, movies[1].Id, movies[1].Id, movies[2].Id]));
		context.ChangeTracker.Clear();

		// Assert
		Assert.Equal(UpdateActorResult.Success, result);

		var actorFromDb = await context.Actors
			.Include(a => a.Movies)
			.FirstAsync(a => a.Id == actor.Id);

		Assert.Equal("Test 99", actorFromDb.Name);
		Assert.Equal("Test99.jpg", actorFromDb.ImageURL);
		Assert.Equal(3, actorFromDb.Movies.Count);

		Assert.Contains(actorFromDb.Movies, m => m.Id == movies[0].Id);
		Assert.Contains(actorFromDb.Movies, m => m.Id == movies[1].Id);
		Assert.Contains(actorFromDb.Movies, m => m.Id == movies[2].Id);
	}
}