namespace MovieApp.Dtos;

public record GenreDto(
	int Id,
	string Name,
	bool ChildrenSafe
);
