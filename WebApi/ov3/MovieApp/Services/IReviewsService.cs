using MovieApp.Dtos.Reviews;
using MovieApp.Enums;
using MovieApp.Results;

namespace MovieApp.Services;

public interface IReviewsService
{
	Task<ReviewDetailedDto?> GetReviewById(int id);
	Task<bool> DeleteReview(int id);
	Task<GenericResult<bool, UpdateReviewErrors>> UpdateReview(int id, ReviewUpdateDto updateData);
	Task<GenericResult<PaginatedResult<ReviewDto>, ReviewsForMovieByIdErrors>> GetReviewsForMovieById(int movieId, int page, int itemsPerPage);
	Task<GenericResult<ReviewDto, CreateReviewErrors>> CreateReviewForMovieById(int movieId, ReviewCreationDto reviewData);
}