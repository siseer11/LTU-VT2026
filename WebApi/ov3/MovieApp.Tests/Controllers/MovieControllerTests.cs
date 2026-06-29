using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApp.Controllers;
using MovieApp.Dtos.Movies;
using MovieApp.Enums;
using MovieApp.Results;
using MovieApp.Services;

namespace MovieApp.Tests.Controllers;

public class MovieControllersTests
{
	private readonly Mock<IMovieService> _serviceMock;
	private readonly MoviesController _controller;

	public MovieControllersTests()
	{
		_serviceMock = new Mock<IMovieService>();
		_controller = new MoviesController(_serviceMock.Object);
	}

	private static PaginatedResult<MovieDto> PaginatedResponse(List<MovieDto>? actorsList, int currentPage = 1, int itemsPerPage = 10, int totalItemsCount = 50)
	{
		return new PaginatedResult<MovieDto>()
		{
			Data = actorsList ?? [],
			Pagination = new(currentPage, itemsPerPage, totalItemsCount)
		};
	}

	private static MovieFullInfoDto CreateMovieDto(
		int id = 1,
		string title = "Test")
	{
		return new MovieFullInfoDto(
				id,
				title,
				2025,
				"Test.jpg",
				"Horror",
				true,
				"Some text",
				"Ro",
				0,
				[],
				[]
		);
	}

	private static MovieCreationDto CreateMovieCreationData(
		List<int> Actors,
		int GenreId = 1
	)
	{
		return new()
		{
			Title = "Test",
			ImageURL = "Test.jpg",
			Language = "Ro",
			Synopsis = "Best movie",
			Budget = 0,
			Actors = Actors,
			GenreId = GenreId
		};
	}

	private static MovieUpdateDto CreateMovieUpdateDto()
	{
		return new()
		{
			Actors = [],
			ImageURL = "Test.jpg",
			Language = "EN",
			Synopsis = "Worst movie",
			Title = "Wtc",
			Budget = 1,
			GenreId = 1,
			Year = 1951
		};
	}

	[Fact]
	public async Task GetMovieById_ReturnsNotFound_WhenNoMovie()
	{
		// Arrange
		_serviceMock
			.Setup(x => x.GetMovieById(1))
			.ReturnsAsync((MovieFullInfoDto?)null);

		// Act
		var response = await _controller.GetMovieById(1);

		// Assert
		Assert.IsType<NotFoundResult>(response.Result);
		_serviceMock.Verify(
			a => a.GetMovieById(1),
			Times.Once
		);
	}

	[Fact]
	public async Task GetMovieById_ReturnsOk_WhenMovieFound()
	{
		// Arrange
		_serviceMock
			.Setup(
				x => x.GetMovieById(1)
			)
			.ReturnsAsync(CreateMovieDto(1, "Test"));

		// Act
		var response = await _controller.GetMovieById(1);

		// Assert
		var okResponse = Assert.IsType<OkObjectResult>(response.Result);
		Assert.IsType<MovieFullInfoDto>(okResponse.Value);

		_serviceMock.Verify(
			x => x.GetMovieById(1),
			Times.Once
		);
	}


	[Fact]
	public async Task GetMovies_ReturnsOk()
	{
		// Arrange
		_serviceMock
			.Setup(x => x.GetMovies(null, null, null, 1, 10))
			.ReturnsAsync(PaginatedResponse(null));

		// Act
		var response = await _controller.GetMovies(null, null, null);

		// Assert
		var okReponse = Assert.IsType<OkObjectResult>(response.Result);
		Assert.IsType<PaginatedResult<MovieDto>>(okReponse.Value);

		_serviceMock.Verify(
			x => x.GetMovies(null, null, null, 1, 10),
			Times.Once
		);
	}

	[Fact]
	public async Task GetMovies_PassesPropsToService()
	{
		// Arrange
		_serviceMock
			.Setup(x => x.GetMovies("Action", "Test Movie", "Leo", 1, 10))
			.ReturnsAsync(PaginatedResponse(null));


		// Act
		await _controller.GetMovies("Action", "Test Movie", "Leo");

		// Assert
		_serviceMock.Verify(
			x => x.GetMovies("Action", "Test Movie", "Leo", 1, 10),
			Times.Once
		);
	}

	[Fact]
	public async Task AddMovie_ReturnsBadRequest_WhenGenreNotFound()
	{
		// Arrange
		var movieToAdd = CreateMovieCreationData([], 1);
		_serviceMock
			.Setup(x => x.AddMovie(movieToAdd))
			.ReturnsAsync(
				new GenericResult<MovieFullInfoDto, AddMovieErrors>()
				{
					Success = false,
					ErrorCode = AddMovieErrors.GenreNotFound
				});

		// Act
		var response = await _controller.AddMovie(movieToAdd);

		// Assert
		Assert.IsType<BadRequestObjectResult>(response.Result);

		_serviceMock
			.Verify(x => x.AddMovie(movieToAdd), Times.Once);
	}

	[Fact]
	public async Task AddMovie_ReturnsBadRequest_WhenNotAllActorsExist()
	{
		// Arrange
		var movieToAdd = CreateMovieCreationData([], 1);
		_serviceMock
			.Setup(x => x.AddMovie(movieToAdd))
			.ReturnsAsync(
				new GenericResult<MovieFullInfoDto, AddMovieErrors>()
				{
					Success = false,
					ErrorCode = AddMovieErrors.NotAllActorsExist
				});

		// Act
		var response = await _controller.AddMovie(movieToAdd);

		// Assert
		Assert.IsType<BadRequestObjectResult>(response.Result);

		_serviceMock
			.Verify(x => x.AddMovie(movieToAdd), Times.Once);
	}

	[Fact]
	public async Task AddMovie_ReturnsCretedAtRoute()
	{
		// Arrange
		var movieToAdd = CreateMovieCreationData([], 1);
		_serviceMock
			.Setup(x => x.AddMovie(movieToAdd))
			.ReturnsAsync(
				new GenericResult<MovieFullInfoDto, AddMovieErrors>()
				{
					Success = true,
					Data = CreateMovieDto()
				});

		// Act
		var response = await _controller.AddMovie(movieToAdd);

		// Assert
		var createdResult = Assert.IsType<CreatedAtRouteResult>(response.Result);

		Assert.Equal("GetMovieById", createdResult.RouteName);

		Assert.IsType<MovieFullInfoDto>(createdResult.Value);

		_serviceMock
			.Verify(x => x.AddMovie(movieToAdd), Times.Once);
	}

	[Fact]
	public async Task DeleteMovie_ReturnsNoContent()
	{
		// Arrange
		_serviceMock
			.Setup(x => x.DeleteMovie(1));

		// Act
		var response = await _controller.DeleteMovie(1);

		// Assert
		Assert.IsType<NoContentResult>(response);

		_serviceMock.Verify(
			x => x.DeleteMovie(1),
			Times.Once
		);
	}

	[Theory]
	[InlineData(
		FullyUpdateMovieErrors.GenreNotFound,
		typeof(BadRequestObjectResult)
	)]
	[InlineData(
		FullyUpdateMovieErrors.NotAllActorsExist,
		typeof(BadRequestObjectResult)
	)]
	[InlineData(
		FullyUpdateMovieErrors.MovieNotFound,
		typeof(NotFoundResult)
	)]
	public async Task FullyUpdateMovie_ReturnsExpectedResult_WhenErrorOccurs(
		FullyUpdateMovieErrors error,
		Type expectedResult
	)
	{
		// Arrange
		var movieUpdateData = CreateMovieUpdateDto();
		_serviceMock
			.Setup(x => x.FullyUpdateMovie(1, movieUpdateData))
			.ReturnsAsync(
				new GenericResult<bool, FullyUpdateMovieErrors>()
				{
					Success = false,
					ErrorCode = error
				});

		// Act
		var response = await _controller.FullyUpdateMovie(1, movieUpdateData);

		// Assert
		Assert.IsType(expectedResult, response);

		_serviceMock.Verify(
			x => x.FullyUpdateMovie(1, movieUpdateData),
			Times.Once
		);
	}

	[Fact]
	public async Task FullyUpdateMovie_ReturnsNoContent()
	{
		// Arrange
		var movieUpdateData = CreateMovieUpdateDto();
		_serviceMock
			.Setup(x => x.FullyUpdateMovie(1, movieUpdateData))
			.ReturnsAsync(
				new GenericResult<bool, FullyUpdateMovieErrors>()
				{
					Success = true,
					Data = true
				});

		// Act
		var response = await _controller.FullyUpdateMovie(1, movieUpdateData);

		// Assert
		Assert.IsType<NoContentResult>(response);

		_serviceMock.Verify(
			x => x.FullyUpdateMovie(1, movieUpdateData),
			Times.Once
		);
	}

	[Theory]
	[InlineData(
		AddActorToMovieCastErrors.MovieNotFound,
		typeof(NotFoundObjectResult)
	)]
	[InlineData(
		AddActorToMovieCastErrors.ActorNotFound,
		typeof(NotFoundObjectResult)
	)]
	[InlineData(
		AddActorToMovieCastErrors.ActorAlreadyInMovie,
		typeof(BadRequestObjectResult)
	)]
	public async Task AddActorToMovieCast_ReturnsExpectedType_WhenErrorOccurs(
		AddActorToMovieCastErrors error,
		Type expectedResultType
	)
	{
		// Arrange
		_serviceMock
			.Setup(x => x.AddActorToMovieCast(1, 1))
			.ReturnsAsync(
				new GenericResult<bool, AddActorToMovieCastErrors>()
				{
					Success = false,
					ErrorCode = error
				}
			);

		// Act
		var response = await _controller.AddActorToMovieCast(1, 1);

		// Assert
		Assert.IsType(expectedResultType, response);

		_serviceMock.Verify(
			x => x.AddActorToMovieCast(1, 1),
			Times.Once
		);
	}

	[Fact]
	public async Task AddActorToMovieCast_ReturnsNoContent()
	{
		// Arrange
		_serviceMock
			.Setup(x => x.AddActorToMovieCast(1, 1))
			.ReturnsAsync(
				new GenericResult<bool, AddActorToMovieCastErrors>()
				{
					Success = true,
					Data = true
				}
			);

		// Act
		var response = await _controller.AddActorToMovieCast(1, 1);

		// Assert
		Assert.IsType<NoContentResult>(response);

		_serviceMock.Verify(
			x => x.AddActorToMovieCast(1, 1),
			Times.Once
		);
	}
}
