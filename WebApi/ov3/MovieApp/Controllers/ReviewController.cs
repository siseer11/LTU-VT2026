using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos.Movies;
using MovieApp.Dtos.Reviews;
using MovieApp.Dtos.Users;
using MovieApp.Models;

namespace MovieApp.Controllers;

[Route("api/")]
[ApiController]
public class ReviewController(AppDbContext context) : ControllerBase
{
	private readonly AppDbContext _context = context;

	public async Task<bool> MovieDoesNotExists(int movieId)
	{
		var movie = await _context.Movies.FindAsync(movieId);
		return movie is null;
	}

	[HttpGet("reviews/{id:int}", Name = "ReviewById")]
	public async Task<ActionResult<ReviewDetailedDto>> GetReviewById(int id)
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

		if (review is null)
			return NotFound(new { error = "No review found!" });

		return Ok(review);
	}

	[HttpDelete("reviews/{id:int}")]
	public async Task<ActionResult> DeleteReview(int id)
	{
		await _context.Reviews
			.Where(r => r.Id == id)
			.ExecuteDeleteAsync();

		return NoContent();
	}

	[HttpPut("reviews/{id:int}")]
	public async Task<ActionResult> UpdateReview(int id, ReviewUpdateDto updateData)
	{
		var review = await _context.Reviews.FindAsync(id);
		if (review is null)
			return NotFound(new { error = "No review found!" });

		review.Edited = true;
		review.EditedAt = DateTime.Now.ToUniversalTime();
		review.Rating = updateData.Rating;
		review.Comment = updateData.Comment;

		await _context.SaveChangesAsync();

		return NoContent();
	}

	[HttpGet("movies/{movieId:int}/reviews")]
	public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForMovieById(int movieId)
	{
		//checks
		if (await MovieDoesNotExists(movieId))
			return NotFound(new { error = "No movie found!" });


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

		return Ok(reviews);
	}

	[HttpPost("movies/{movieId:int}/reviews")]
	public async Task<ActionResult<ReviewDto>> CreateReviewForMovieById(int movieId, ReviewCreationDto reviewData)
	{
		//checks
		if (await MovieDoesNotExists(movieId))
			return NotFound(new { error = "No movie found!" });

		var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == reviewData.ReviewerId);
		if (user is null)
			return NotFound(new { error = "No user found!" });


		//create and save
		Review reviewToAdd = new()
		{
			MovieId = movieId,
			Comment = reviewData.Comment,
			Rating = reviewData.Rating,
			ReviewerId = reviewData.ReviewerId,
		};

		var res = await _context.Reviews.AddAsync(reviewToAdd);
		await _context.SaveChangesAsync();

		var newReview = res.Entity;

		return CreatedAtRoute(
			"ReviewById",
			new { id = newReview.Id },
			new ReviewDto(
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
		);
	}
}