using MovieApp.Models;

namespace MovieApp.Data;

public static class SeedData
{
	public static async Task Initialize(AppDbContext context)
	{
		if (context.Movies.Any())
			return;


		#region genre
		Genre Action = new() { Name = "Action", ChildrenSafe = false };
		Genre Animation = new() { Name = "Animation", ChildrenSafe = true };
		Genre SciFi = new() { Name = "Sci-Fi", ChildrenSafe = false };
		List<Genre> genres =
		[
			Action,
			Animation,
			SciFi,
			new() { Name = "Comedy", ChildrenSafe = true },
			new() {  Name = "Drama", ChildrenSafe = false }
		];
		#endregion

		#region users
		User Alice = new() { Name = "Alice", ImageURL = "https://media.themoviedb.org/t/p/w300_and_h450_face/jCFOlFAjNPMkX9pJF2Au0cquZ6v.jpg" };
		User Bob = new()
		{
			Name = "Bob",
			ImageURL = "https://media.themoviedb.org/t/p/w300_and_h450_face/3RX8OBqt3gbvFwKYZqiom4O3Ta6.jpg"
		};

		User Dave = new()
		{
			Name = "Dave",
			ImageURL = "https://media.themoviedb.org/t/p/w300_and_h450_face/feRkUWfs2LpVzULMdkpBHN5JYdM.jpg",
			IsAHater = true
		};

		List<User> users = [Alice, Bob, Dave];
		#endregion

		#region actors
		Actor Actor1 = new()
		{
			Name = "Keanu Reeves",
			ImageURL = "https://media.themoviedb.org/t/p/w300_and_h450_face/kEoUZKEG7dzbCESDjd0CKAN1r0n.jpg",
			BirthDate = new DateOnly(1964, 9, 2)
		};
		Actor Actor2 = new()
		{
			Name = "Carrie-Anne Moss",
			ImageURL = "https://media.themoviedb.org/t/p/w300_and_h450_face/xD4jTA3KmVp5Rq3aHcymL9DUGjD.jpg",
			BirthDate = new DateOnly(1967, 8, 21)
		};
		Actor Actor3 = new()
		{
			Name = "Tom Hanks",
			ImageURL = "https://media.themoviedb.org/t/p/w300_and_h450_face/oFvZoKI6lvU03n4YoNGAll9rkas.jpg",
			BirthDate = new DateOnly(1956, 7, 9)
		};
		var actors = new List<Actor>
		{
			Actor1,
			Actor2,
			Actor3
		};

		#endregion

		#region movies
		Movie Matrix = new()
		{
			Title = "The Matrix",
			Year = 1999,
			ImageURL = "https://image.tmdb.org/t/p/w600_and_h900_face/aOIuZAjPaRIE6CMzbazvcHuHXDc.jpg",
			Genre = SciFi,
			Actors = [Actor1, Actor2]
		};
		Movie ToyStory = new()
		{
			Title = "Toy Story",
			Year = 1995,
			ImageURL = "https://image.tmdb.org/t/p/w600_and_h900_face/uXDfjJbdP4ijW5hWSBrPrlKpxab.jpg",
			Genre = Animation,
			Actors = [Actor3]
		};
		Movie JohnWick = new()
		{
			Title = "John Wick",
			Year = 2014,
			ImageURL = "https://image.tmdb.org/t/p/w600_and_h900_face/wXqWR7dHncNRbxoEGybEy7QTe9h.jpg",
			Genre = Action,
			Actors = [Actor1]
		};

		var movies = new List<Movie>
		{ Matrix,
			ToyStory,
			JohnWick
		};
		#endregion

		#region movieDetails
		var movieDetails = new List<MovieDetails>
		{
			new()
			{
					Movie = Matrix,
					Language = "English",
					Budget = 63000000,
					Synopsis = "A hacker discovers reality is a simulation."
			},
			new()
			{
					Movie = ToyStory,
					Language = "English",
					Budget = 30000000,
					Synopsis = "Toys come to life when humans are away."
			},
			new()
			{
					Movie = JohnWick,
					Language = "English",
					Budget = 20000000,
					Synopsis = "An ex-hitman seeks revenge."
			}
		};
		#endregion

		#region reviews
		var reviews = new List<Review>
		{
			new()
			{
					Rating = 5,
					Comment = "One of the best sci-fi movies ever.",
					Movie = Matrix,
					Reviewer = Bob,
					CreatedAt = new(2026,01,18,10,05,21)
			},

			new()
			{
					Rating = 4,
					Comment = "Great action and visuals.",
					Movie = Matrix,
					Reviewer = Alice,
					CreatedAt = new(2026,03,11,05,00,21)
			},

			new()
			{
					Rating = 1,
					Comment = "Overrated.",
					Movie = Matrix,
					Reviewer = Dave,
					CreatedAt = new(2025,11,15,11,10,21)
			},

			new()
			{
					Rating = 5,
					Comment = "Perfect family movie.",
					Movie = ToyStory,
					Reviewer = Dave,
					CreatedAt = new(2026,02,27,09,01,00)
			}
		};
		#endregion

		context.AddRange(genres);
		context.AddRange(actors);
		context.AddRange(users);
		context.AddRange(movies);
		context.AddRange(movieDetails);
		context.AddRange(reviews);

		await context.SaveChangesAsync();
	}
}