using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApp.Controllers;
using MovieApp.Dtos.Actors;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Services;

namespace MovieApp.Tests.Controllers;

public class ActorsControllerTests
{
	[Fact]
	public async Task GetActorById_ReturnsOk_WhenActorExists()
	{
		// Arrange
		var mockService = new Mock<IActorService>();

		mockService
			.Setup(x => x.GetActorById(1))
			.ReturnsAsync(new ActorDetailedDto(
				1, "Test", "Test", new DateOnly(), []
			));

		var controller = new ActorsController(mockService.Object);

		// Act
		var result = await controller.GetActorById(1);

		// Assert
		mockService.Verify(
			x => x.GetActorById(1),
			Times.Once
		);
		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		Assert.IsType<ActorDetailedDto>(okResult.Value);
	}

	[Fact]
	public async Task GetActorById_ReturnsNotFound_WhenActorDoesNotExists()
	{
		// Arrange
		var mockService = new Mock<IActorService>();

		mockService
			.Setup(x => x.GetActorById(1))
			.ReturnsAsync((ActorDetailedDto?)null);

		var controller = new ActorsController(mockService.Object);

		// Act
		var result = await controller.GetActorById(1);

		// Assert
		mockService.Verify(
			x => x.GetActorById(1),
			Times.Once
		);
		Assert.IsType<NotFoundObjectResult>(result.Result);
	}

	[Fact]
	public async Task GetActors_ReturnsOk()
	{
		// Arrage
		var mockService = new Mock<IActorService>();

		List<ActorDto> mockActorList = [
			new ActorDto(1, "Test 1", "test1.jpg", new DateOnly()),
			new ActorDto(2, "Test 2", "test2.jpg", new DateOnly())
		];

		mockService
			.Setup(x => x.GetActors(null))
			.ReturnsAsync(mockActorList);

		var controller = new ActorsController(mockService.Object);

		// Act
		var result = await controller.GetActors(null);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result.Result);

		var returnedActors = Assert.IsType<IEnumerable<ActorDto>>(okResult.Value, exactMatch: false);

		Assert.Equal(2, returnedActors.Count());
	}

	[Fact]
	public async Task GetActors_PassesNameToService()
	{
		// Arange
		var mockService = new Mock<IActorService>();

		mockService
			.Setup(x => x.GetActors("Nick"))
			.ReturnsAsync(new List<ActorDto>());

		var controller = new ActorsController(mockService.Object);

		// Act
		await controller.GetActors("Nick");

		// Assert
		mockService.Verify(
			x => x.GetActors("Nick"),
			Times.Once
		);
	}

	[Fact]
	public async Task CreatActor_ReturnsCreatedAtRoute()
	{
		// Arrage
		ActorCreationDto newActorInputData = new("Test", "img.png", new DateOnly());
		var mockService = new Mock<IActorService>();

		mockService
			.Setup(x => x.CreateActor(newActorInputData))
			.ReturnsAsync(new Actor() { Id = 1, ImageURL = "img.png", Name = "Test", BirthDate = new DateOnly(), Movies = [] });

		var controller = new ActorsController(mockService.Object);

		// Act
		var res = await controller.CreateActor(newActorInputData);

		// Assert
		var createdResult = Assert.IsType<CreatedAtRouteResult>(res.Result);

		Assert.Equal("GetActorById", createdResult.RouteName);

		var actor = Assert.IsType<ActorDto>(createdResult.Value);

		Assert.Equal(1, actor.Id);
	}

	[Fact]
	public async Task DeleteActor_ReturnsNoContent()
	{
		// Arrange
		var mockService = new Mock<IActorService>();

		mockService
			.Setup(x => x.DeleteActor(1));

		var controller = new ActorsController(mockService.Object);
		// Act
		var result = await controller.DeleteActor(1);

		// Assert
		Assert.IsType<NoContentResult>(result);
		mockService.Verify(
			x => x.DeleteActor(1),
			Times.Once
		);
	}

	[Fact]
	public async Task UpdateActor_ReturnsNotFound_WhenActorDoesNotExist()
	{
		// Arrange
		var mockService = new Mock<IActorService>();
		var updateData = new ActorUpdateDto("Tt", "T.jpg", new DateOnly(), new List<int>());

		mockService
			.Setup(x => x.UpdateActor(1, updateData))
			.ReturnsAsync(UpdateActorResult.ActorNotFound);

		var controller = new ActorsController(mockService.Object);

		// Act
		var response = await controller.UpdateActor(1, updateData);

		// Assert
		Assert.IsType<NotFoundObjectResult>(response);
		mockService.Verify(
			x => x.UpdateActor(1, updateData),
			Times.Once
		);
	}

	[Fact]
	public async Task UpdateActor_ReturnsBadRequest_WhenMoviesAreInvalid()
	{
		// Arrange
		var mockService = new Mock<IActorService>();
		var updateData = new ActorUpdateDto("Tt", "T.jpg", new DateOnly(), new List<int>());

		mockService
			.Setup(x => x.UpdateActor(1, updateData))
			.ReturnsAsync(UpdateActorResult.InvalidMovies);

		var controller = new ActorsController(mockService.Object);

		// Act
		var response = await controller.UpdateActor(1, updateData);

		// Assert
		Assert.IsType<BadRequestObjectResult>(response);
		mockService.Verify(
			x => x.UpdateActor(1, updateData),
			Times.Once
		);
	}

	[Fact]
	public async Task UpdateActor_ReturnsNoContent_WhenUpdateSucceeds()
	{
		// Arrange
		var mockService = new Mock<IActorService>();
		var updateData = new ActorUpdateDto("Tt", "T.jpg", new DateOnly(), new List<int>());

		mockService
			.Setup(x => x.UpdateActor(1, updateData))
			.ReturnsAsync(UpdateActorResult.Success);

		var controller = new ActorsController(mockService.Object);

		// Act
		var response = await controller.UpdateActor(1, updateData);

		// Assert
		Assert.IsType<NoContentResult>(response);
		mockService.Verify(
			x => x.UpdateActor(1, updateData),
			Times.Once
		);
	}
}