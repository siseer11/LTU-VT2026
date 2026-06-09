using Games.Api.Models;

const string GetGameEndpointName = "GetGage";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
	new(1, "wOw", "MMO",true, new (1999,11,21)),
	new(2, "Slay the spinare", "Turn based",false, new (2021,11,21)),
	new(3, "LawL", "RIOT",true, new (2005,5,24))
];

app.MapGet("/games", () => games);

app.MapGet("/games/{id}", (int id) =>
{
	var game = games.FirstOrDefault(game => game.Id == id);

	if (game is null)
		return Results.NotFound(new { error = "No game that matches the id could be found!" });

	return Results.Ok(game);
})
.WithName(GetGameEndpointName);

app.MapPost("/games", (GameCreationDto body) =>
{
	GameDto newGame = new(games.Count + 1, body.Name, body.Genre, body.HasMultiplayer, body.ReleaseDate);

	games.Add(newGame);

	return Results.CreatedAtRoute(GetGameEndpointName, new { id = newGame.Id }, newGame);
});

app.MapPut("/games/{id}", (int id, GameUpdateDto gameUpdatedData) =>
{
	// find the game
	int gameIdx = games.FindIndex(game => game.Id == id);

	if (gameIdx == -1)
	{
		return Results.NotFound("Game could not be found");
	}

	// update
	games[gameIdx] = new GameDto(games[gameIdx].Id, gameUpdatedData.Name, gameUpdatedData.Genre, gameUpdatedData.HasMultiplayer, gameUpdatedData.ReleaseDate);

	return Results.NoContent();
});


app.MapDelete("/games/{id}", (int id) =>
{
	games.RemoveAll(game => game.Id == id);

	return Results.NoContent();
});

app.Run();
