using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos.Reviews;
using MovieApp.Services;

namespace MovieApp.Controllers;

[Route("api/")]
[ApiController]
public class ReviewsController(IReviewsService service) : ControllerBase
{
	private readonly IReviewsService _service = service;
	private const int maxNumberOfReviewsPerPage = 20;

	private int GetReviewerIdFromToken()
	{
		// User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

		_ = int.TryParse(userIdFromToken, out int id);

		return id;
	}

	[HttpGet("reviews/{id:int}", Name = "ReviewById")]
	public async Task<ActionResult<ReviewDetailedDto>> GetReviewById(int id)
	{
		var review = await _service.GetReviewById(id);

		if (review is null)
			return NotFound(new { error = "No review found!" });

		return Ok(review);
	}

	[HttpDelete("reviews/{id:int}")]
	[Authorize]
	public async Task<ActionResult> DeleteReview(int id)
	{
		await _service.DeleteReview(id);

		return NoContent();
	}

	[HttpPut("reviews/{id:int}")]
	[Authorize]
	public async Task<ActionResult> UpdateReview(int id, ReviewUpdateDto updateData)
	{
		var result = await _service.UpdateReview(id, updateData, GetReviewerIdFromToken());

		if (result.Success == false)
		{
			return result.ErrorCode switch
			{
				Enums.UpdateReviewErrors.ReviewNotFound =>
					NotFound(),
				Enums.UpdateReviewErrors.WrongUserTryingToUpdate =>
					Forbid(),
				_ =>
					StatusCode(500)
			};
		}

		return NoContent();
	}

	[HttpGet("movies/{movieId:int}/reviews")]
	public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForMovieById(int movieId, int page = 1, int perPage = 10)
	{
		if (perPage > maxNumberOfReviewsPerPage)
			perPage = maxNumberOfReviewsPerPage;

		var result = await _service.GetReviewsForMovieById(movieId, page, perPage);

		if (result.Success == false)
		{
			return result.ErrorCode switch
			{
				Enums.ReviewsForMovieByIdErrors.MovieNotFound =>
					NotFound(),
				_ =>
					StatusCode(500)
			};
		}

		return Ok(result.Data);
	}

	[HttpPost("movies/{movieId:int}/reviews")]
	[Authorize]
	public async Task<ActionResult<ReviewDto>> CreateReviewForMovieById(int movieId, ReviewCreationDto reviewData)
	{
		var result = await _service.CreateReviewForMovieById(movieId, GetReviewerIdFromToken(), reviewData);

		if (result.Success == false)
		{
			return result.ErrorCode switch
			{
				Enums.CreateReviewErrors.MovieNotFound =>
					NotFound(new { error = "No movie found!" }),
				Enums.CreateReviewErrors.UserNotFound =>
					NotFound(new { error = "No user found!" }),
				_ =>
					StatusCode(500)
			};
		}

		return CreatedAtRoute(
			"ReviewById",
			new { id = result.Data!.Id },
			result.Data
		);
	}
}