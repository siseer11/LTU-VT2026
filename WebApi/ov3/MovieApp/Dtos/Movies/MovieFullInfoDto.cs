using MovieApp.Dtos.Actors;
namespace MovieApp.Dtos.Movies;

public record MovieFullInfoDto(
	int Id,
	string Title,
	int Year,
	string ImageURL,
	string Genre,
	bool ChildrenSafe,
	string Synopsis,
	string Language,
	int Budget,
	List<ActorDto> Actors,
	List<ReviewDto> Reviews
);