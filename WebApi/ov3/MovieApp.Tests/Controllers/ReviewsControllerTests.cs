using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApp.Controllers;
using MovieApp.Dtos.Movies;
using MovieApp.Dtos.Reviews;
using MovieApp.Dtos.Users;
using MovieApp.Results;
using MovieApp.Services;

namespace MovieApp.Tests.Controllers;

public class ReviewsControllerTests
{
	ReviewsController _controller;
	Mock<IReviewsService> _serviceMock;

	public ReviewsControllerTests()
	{
		_serviceMock = new Mock<IReviewsService>();
		_controller = new ReviewsController(_serviceMock.Object);
	}

	private static UserDto CreateReviewerDto()
	{
		return new(1, "Test", "T.jpg", true);
	}

	private static MovieDto CreateMovieDto()
	{
		return new(1, "T", 2022, "M.jpg", "Some", true);
	}

	[Fact]
	public async Task GetReviewById_ReturnsNotFound_IfNoReviewFound()
	{
		// Arrange
		_serviceMock
			.Setup(x => x.GetReviewById(1))
			.ReturnsAsync((ReviewDetailedDto?)null);
		// Act
		var response = await _controller.GetReviewById(1);

		// Assert
		Assert.IsType<NotFoundObjectResult>(response.Result);
		_serviceMock.Verify(x => x.GetReviewById(1), Times.Once);
	}

	[Fact]
	public async Task GetReviewById_ReturnsOk_IfReviewFound()
	{
		// Arrange
		int reviewId = 1;
		_serviceMock
			.Setup(x => x.GetReviewById(1))
			.ReturnsAsync(
				new ReviewDetailedDto(reviewId, 2, "testMsg", false, new DateTime(), new DateTime(), CreateReviewerDto(), CreateMovieDto())
			);

		// Act
		var response = await _controller.GetReviewById(1);

		// Assert
		var okResponse = Assert.IsType<OkObjectResult>(response.Result);
		var review = Assert.IsType<ReviewDetailedDto>(okResponse.Value);

		Assert.Equal("testMsg", review.Comment);
		Assert.Equal(reviewId, review.Id);

		_serviceMock.Verify(x => x.GetReviewById(1), Times.Once);
	}

	[Fact]
	public async Task DeleteReview_ReturnsNoContent()
	{
		// Arrange
		_serviceMock.Setup(x => x.DeleteReview(1));

		// Act
		var response = await _controller.DeleteReview(1);

		// Assert
		Assert.IsType<NoContentResult>(response);

		_serviceMock.Verify(x => x.DeleteReview(1), Times.Once);
	}

	[Fact]
	public async Task UpdateReview_ReturnsNotFound_WhenReviewNotFound()
	{
		// Arrange
		ReviewUpdateDto reviewUpdateDto = new(5, "Not good");
		_serviceMock
			.Setup(x => x.UpdateReview(1, reviewUpdateDto))
			.ReturnsAsync(new GenericResult<bool, Enums.UpdateReviewErrors>() { Success = false, ErrorCode = Enums.UpdateReviewErrors.ReviewNotFound });

		// Act
		var response = await _controller.UpdateReview(1, reviewUpdateDto);

		// Assert
		Assert.IsType<NotFoundResult>(response);

		_serviceMock.Verify(x => x.UpdateReview(1, reviewUpdateDto), Times.Once);
	}

	[Fact]
	public async Task UpdateReview_ReturnsNoContent()
	{
		// Arrange
		ReviewUpdateDto reviewUpdateDto = new(5, "Not good");
		_serviceMock
			.Setup(x => x.UpdateReview(1, reviewUpdateDto))
			.ReturnsAsync(new GenericResult<bool, Enums.UpdateReviewErrors>() { Success = true, Data = true });

		// Act
		var response = await _controller.UpdateReview(1, reviewUpdateDto);

		// Assert
		Assert.IsType<NoContentResult>(response);

		_serviceMock.Verify(x => x.UpdateReview(1, reviewUpdateDto), Times.Once);
	}

	[Fact]
	public async Task GetReviewsForMovieById_ReturnsNotFound_WhenMovieNotFound()
	{
		// Arrange
		_serviceMock
			.Setup(x => x.GetReviewsForMovieById(1))
			.ReturnsAsync(new GenericResult<IEnumerable<ReviewDto>, Enums.ReviewsForMovieByIdErrors>() { Success = false, ErrorCode = Enums.ReviewsForMovieByIdErrors.MovieNotFound });

		// Act
		var response = await _controller.GetReviewsForMovieById(1);

		// Assert
		Assert.IsType<NotFoundResult>(response.Result);

		_serviceMock.Verify(x => x.GetReviewsForMovieById(1), Times.Once);
	}

	[Fact]
	public async Task GetReviewsForMovieById_ReturnsOk_WhenMovieFound()
	{
		// Arrange
		_serviceMock
			.Setup(x => x.GetReviewsForMovieById(1))
			.ReturnsAsync(new GenericResult<IEnumerable<ReviewDto>, Enums.ReviewsForMovieByIdErrors>()
			{
				Success = true,
				Data = [new ReviewDto(1, 1, "no good!", false, new DateTime(), new DateTime(), CreateReviewerDto())]
			});

		// Act
		var response = await _controller.GetReviewsForMovieById(1);

		// Assert
		var okResponse = Assert.IsType<OkObjectResult>(response.Result);
		var reviews = Assert.IsType<IEnumerable<ReviewDto>>(okResponse.Value, exactMatch: false);

		Assert.Single(reviews);

		_serviceMock.Verify(x => x.GetReviewsForMovieById(1), Times.Once);
	}

	[Theory]
	[InlineData(
		Enums.CreateReviewErrors.MovieNotFound,
		typeof(NotFoundObjectResult)
	)]
	[InlineData(
		Enums.CreateReviewErrors.UserNotFound,
		typeof(NotFoundObjectResult)
	)]
	public async Task CreateReviewForMovieById_ReturnsCorrectResponse_WhenErrorOccurrs(Enums.CreateReviewErrors error, Type expectedReturnType)
	{
		// Arrange
		ReviewCreationDto reviewData = new(2, 1, "worst!");
		_serviceMock
			.Setup(x => x.CreateReviewForMovieById(1, reviewData))
			.ReturnsAsync(new GenericResult<ReviewDto, Enums.CreateReviewErrors>() { Success = false, ErrorCode = error });

		// Act
		var response = await _controller.CreateReviewForMovieById(1, reviewData);

		// Assert
		Assert.IsType(expectedReturnType, response.Result);

		_serviceMock.Verify(x => x.CreateReviewForMovieById(1, reviewData), Times.Once);
	}

	[Fact]
	public async Task CreateReviewForMovieById_ReturnsCreatedAtRoute()
	{
		// Arrange
		ReviewCreationDto reviewData = new(2, 1, "worst!");
		_serviceMock
			.Setup(x => x.CreateReviewForMovieById(1, reviewData))
			.ReturnsAsync(new GenericResult<ReviewDto, Enums.CreateReviewErrors>()
			{
				Success = true,
				Data = new(2, 1, "worst!", false, new DateTime(), new DateTime(), CreateReviewerDto())
			}
			);

		// Act
		var response = await _controller.CreateReviewForMovieById(1, reviewData);

		// Assert
		var okResponse = Assert.IsType<CreatedAtRouteResult>(response.Result);
		Assert.Equal("ReviewById", okResponse.RouteName);
		Assert.IsType<ReviewDto>(okResponse.Value);

		_serviceMock.Verify(x => x.CreateReviewForMovieById(1, reviewData), Times.Once);
	}
}
