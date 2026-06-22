using MovieApp.Dtos.Movies;
using MovieApp.Enums;
using MovieApp.Results;

namespace MovieApp.Services;

public interface IMovieService
{
	Task<IEnumerable<MovieDto>> GetMovies(string? genre, string? title, string? actor);
	Task<MovieFullInfoDto?> GetMovieById(int id);
	Task<GenericResult<MovieFullInfoDto, AddMovieErrors>> AddMovie(MovieCreationDto newMovieData);
	Task<GenericResult<bool, FullyUpdateMovieErrors>> FullyUpdateMovie(int id, MovieUpdateDto updateData);
	Task DeleteMovie(int id);
	Task<GenericResult<bool, AddActorToMovieCastErrors>> AddActorToMovieCast(int movieId, int actorId);
}