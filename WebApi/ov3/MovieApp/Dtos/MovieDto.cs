namespace MovieApp.Dtos;

public record MovieDto(
	int Id,
	string Title,
	int Year,
	string ImageURL,
	string Genre,
	bool ChildrenSafe
);