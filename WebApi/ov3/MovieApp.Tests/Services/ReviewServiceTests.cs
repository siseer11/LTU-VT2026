using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos.Reviews;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Tests.Helpers;

namespace MovieApp.Tests.Services;

public class ReviewServiceTests
{

	private AppDbContext _context;
	private ReviewsService _service;
	public ReviewServiceTests()
	{
		_context = TestDbContextFactory.Create();
		_service = new ReviewsService(_context);
	}

	private async Task<Movie> CreateAndSaveMovie()
	{
		Genre genre = new() { ChildrenSafe = true, Name = "Test" };
		await _context.Genres.AddAsync(genre);

		Movie movie = new()
		{
			ImageURL = "Test.jpg",
			Title = "My Movie",
			Genre = genre,
			Year = 2005
		};
		await _context.Movies.AddAsync(movie);
		await _context.SaveChangesAsync();

		return movie;
	}

	private async Task<User> CreateAndSaveReviewer()
	{
		User user = new() { ImageURL = "TestUser.jpg", Name = "Test user", IsAHater = true };
		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		return user;
	}

	private static Review CreateReview(int movieId, int userId, string? Comment = null)
	{
		return new Review()
		{
			Rating = 1,
			CreatedAt = DateTime.UtcNow,
			MovieId = movieId,
			ReviewerId = userId,
			Comment = Comment
		};
	}

	private static List<Review> CreateReviewsList(int movieId, int userId, int numberOfReviews)
	{
		List<Review> listOfReviews = [];
		for (int i = 0; i < numberOfReviews; i++)
			listOfReviews.Add(CreateReview(movieId, userId));

		return listOfReviews;
	}

	private static ReviewCreationDto CreateReviewCreationDto(int ReviewerId, string? Comment = null, int Rating = 1)
	{
		return new(Rating, ReviewerId, Comment);
	}

	private static ReviewUpdateDto CreateReviewUpdateDto(int Rating = 1, string? Comment = null)
	{
		return new(Rating, Comment);
	}


	[Fact]
	public async Task GetReviewsForMovieById_ReturnsErrorCode_WhenNoMovieFound()
	{
		// Arrange

		// Act
		var response = await _service.GetReviewsForMovieById(1);

		// Assert
		Assert.False(response.Success);
		Assert.Equal(ReviewsForMovieByIdErrors.MovieNotFound, response.ErrorCode);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(12)]
	[InlineData(80)]
	public async Task GetReviewsForMovieById_ReturnReviewsList_WhenMovieFound(int numberOfReviews)
	{
		// Arrange
		var movie = await CreateAndSaveMovie();
		var user = await CreateAndSaveReviewer();
		var reviews = CreateReviewsList(movie.Id, user.Id, numberOfReviews);
		await _context.Reviews.AddRangeAsync(reviews);

		await _context.SaveChangesAsync();
		// Act
		var response = await _service.GetReviewsForMovieById(movie.Id);

		// Assert
		Assert.True(response.Success);
		Assert.Equal(numberOfReviews, response.Data!.Count());
	}

	[Fact]
	public async Task GetReviewById_ReturnsNull_IfNoReviewFound()
	{
		// Arrange

		// Act
		var response = await _service.GetReviewById(1);

		// Assert
		Assert.Null(response);
	}

	[Fact]
	public async Task GetReviewById_ReturnsReview_IfReviewFound()
	{
		// Arrange
		string comment = "Amazing movie!";
		var movie = await CreateAndSaveMovie();
		var user = await CreateAndSaveReviewer();
		var review = CreateReview(movie.Id, user.Id, comment);
		await _context.Reviews.AddAsync(review);

		await _context.SaveChangesAsync();

		// Act
		var response = await _service.GetReviewById(review.Id);

		// Assert
		Assert.NotNull(response);
		Assert.IsType<ReviewDetailedDto>(response);
		Assert.Equal(comment, response.Comment);
		Assert.Equal(movie.Id, response.Movie.Id);
		Assert.Equal(user.Id, response.Reviewer.Id);
	}

	[Fact]
	public async Task CreateReviewForMovieById_ReturnsErrorMessage_WhenMovieNotFound()
	{
		// Arrange
		var reviewDto = CreateReviewCreationDto(1);
		// Act
		var response = await _service.CreateReviewForMovieById(1, reviewDto);
		// Assert
		Assert.False(response.Success);
		Assert.Equal(CreateReviewErrors.MovieNotFound, response.ErrorCode);
	}

	[Fact]
	public async Task CreateReviewForMovieById_ReturnsErrorMessage_WhenUserNotFound()
	{
		// Arrange
		var movie = await CreateAndSaveMovie();
		var reviewDto = CreateReviewCreationDto(1);
		// Act
		var response = await _service.CreateReviewForMovieById(movie.Id, reviewDto);
		// Assert
		Assert.False(response.Success);
		Assert.Equal(CreateReviewErrors.UserNotFound, response.ErrorCode);
	}

	[Fact]
	public async Task CreateReviewForMovieById_ReturnsNewlyCreatedReview()
	{
		// Arrange
		string comment = "Best movie!";
		int rating = 1;
		var movie = await CreateAndSaveMovie();
		var reviewer = await CreateAndSaveReviewer();
		var reviewDto = CreateReviewCreationDto(reviewer.Id, comment, rating);

		// Act
		var response = await _service.CreateReviewForMovieById(movie.Id, reviewDto);

		// Assert
		Assert.True(response.Success);
		Assert.IsType<ReviewDto>(response.Data);
		Assert.Equal(response.Data.Comment, comment);
		Assert.Equal(response.Data.Rating, rating);
		Assert.Equal(response.Data.Reviewer.Id, reviewer.Id);

		var reviewInDb = await _context.Reviews.Include(r => r.Reviewer).FirstOrDefaultAsync(r => r.Id == response.Data.Id);
		Assert.NotNull(reviewInDb);
		Assert.Equal(reviewInDb.Comment, comment);
		Assert.Equal(reviewInDb.Rating, rating);
		Assert.Equal(reviewInDb.Reviewer.Id, reviewer.Id);
	}

	[Fact]
	public async Task DeleteReview_RemovesReviewFromDb()
	{
		// Arrange
		var movie = await CreateAndSaveMovie();
		var reviewer = await CreateAndSaveReviewer();
		var review = CreateReview(movie.Id, reviewer.Id);
		_context.Reviews.Add(review);
		await _context.SaveChangesAsync();

		// Act
		await _service.DeleteReview(review.Id);

		// Assert
		var reviewFromDb = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);
		Assert.Null(reviewFromDb);
	}

	[Fact]
	public async Task UpdateReview_ReturnsError_WhenReviewNotFound()
	{
		// Arrange

		// Act
		var response = await _service.UpdateReview(1, CreateReviewUpdateDto());

		// Assert
		Assert.False(response.Success);
		Assert.Equal(UpdateReviewErrors.ReviewNotFound, response.ErrorCode);
	}

	[Fact]
	public async Task UpdateReview_UpdatesTheReviewInTheDatabase()
	{
		// Arrange
		var movie = await CreateAndSaveMovie();
		var reviewer = await CreateAndSaveReviewer();
		var review = CreateReview(movie.Id, reviewer.Id);
		_context.Reviews.Add(review);
		await _context.SaveChangesAsync();

		int rating = 1;
		string comment = "Best Movie";
		var reviewUpdateData = CreateReviewUpdateDto(rating, comment);
		// Act
		var response = await _service.UpdateReview(review.Id, reviewUpdateData);

		// Assert
		Assert.True(response.Success);

		var reviewFromDb = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);
		Assert.Equal(rating, reviewFromDb!.Rating);
		Assert.Equal(comment, reviewFromDb!.Comment);
		Assert.True(reviewFromDb!.Edited);
		Assert.NotNull(reviewFromDb.EditedAt);
	}
}
