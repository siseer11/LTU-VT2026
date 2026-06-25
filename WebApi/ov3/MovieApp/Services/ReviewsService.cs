using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos.Movies;
using MovieApp.Dtos.Reviews;
using MovieApp.Dtos.Users;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Results;

namespace MovieApp.Services;

public class ReviewsService(AppDbContext context) : IReviewsService
{
	private AppDbContext _context = context;

	private async Task<bool> MovieDoesNotExists(int movieId)
	{
		var movie = await _context.Movies.FindAsync(movieId);
		return movie is null;
	}

	public async Task<GenericResult<IEnumerable<ReviewDto>, ReviewsForMovieByIdErrors>> GetReviewsForMovieById(int movieId)
	{
		//checks
		if (await MovieDoesNotExists(movieId))
			return new() { Success = false, ErrorCode = ReviewsForMovieByIdErrors.MovieNotFound };


		var reviews = await _context.Reviews
			.Where(r => r.MovieId == movieId)
			.Select(r => new ReviewDto(
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
					)
				)
			).ToListAsync();

		return new() { Success = true, Data = reviews };
	}

	public async Task<ReviewDetailedDto?> GetReviewById(int id)
	{
		var review = await _context.Reviews
			.Where(r => r.Id == id)
			.Select(r => new ReviewDetailedDto(
				r.Id,
				r.Rating,
				r.Comment,
				r.Edited,
				r.EditedAt,
				r.CreatedAt,
				new UserDto(r.Reviewer.Id, r.Reviewer.Name, r.Reviewer.ImageURL, r.Reviewer.IsAHater),
				new MovieDto(r.Movie.Id, r.Movie.Title, r.Movie.Year, r.Movie.ImageURL, r.Movie.Genre.Name, r.Movie.Genre.ChildrenSafe)
			))
			.FirstOrDefaultAsync();

		return review;
	}

	public async Task<GenericResult<ReviewDto, CreateReviewErrors>> CreateReviewForMovieById(int movieId, ReviewCreationDto reviewData)
	{
		//checks
		if (await MovieDoesNotExists(movieId))
			return new() { Success = false, ErrorCode = CreateReviewErrors.MovieNotFound };

		var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == reviewData.ReviewerId);
		if (user is null)
			return new() { Success = false, ErrorCode = CreateReviewErrors.UserNotFound };


		//create and save
		Review reviewToAdd = new()
		{
			MovieId = movieId,
			Comment = reviewData.Comment,
			Rating = reviewData.Rating,
			ReviewerId = reviewData.ReviewerId,
			CreatedAt = DateTime.UtcNow
		};

		var res = await _context.Reviews.AddAsync(reviewToAdd);
		await _context.SaveChangesAsync();

		var newReview = res.Entity;

		return new()
		{
			Success = true,
			Data = new ReviewDto(
				newReview.Id,
				newReview.Rating,
				newReview.Comment,
				newReview.Edited,
				newReview.EditedAt,
				newReview.CreatedAt,
				new UserDto(
					newReview.Reviewer.Id,
					newReview.Reviewer.Name,
					newReview.Reviewer.ImageURL,
					newReview.Reviewer.IsAHater
				)
			)
		};
	}

	public async Task<bool> DeleteReview(int id)
	{
		await _context.Reviews
			.Where(r => r.Id == id)
			.ExecuteDeleteAsync();

		return true;
	}

	public async Task<GenericResult<bool, UpdateReviewErrors>> UpdateReview(int id, ReviewUpdateDto updateData)
	{
		var review = await _context.Reviews.FindAsync(id);
		if (review is null)
			return new() { Success = false, ErrorCode = UpdateReviewErrors.ReviewNotFound };

		review.Edited = true;
		review.EditedAt = DateTime.Now.ToUniversalTime();
		review.Rating = updateData.Rating;
		review.Comment = updateData.Comment;

		await _context.SaveChangesAsync();
		return new() { Success = true };
	}
}