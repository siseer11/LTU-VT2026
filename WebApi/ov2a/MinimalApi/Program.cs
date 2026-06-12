var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapGet("/api", () => "Server is running");
List<Person> peopleList = new()
{
	new Person(1, "Test Name 1", 31),
	new Person(2, "Test Name 2", 120),
	new Person(3, "Test Name 3", 52),
};

app.MapGet("/api/people", () =>
{
	return Results.Ok(peopleList);
});

app.MapGet("/api/people/{id:int}", (int id) =>
{
	var person = peopleList.FirstOrDefault(p => p.Id == id);

	if (person is null)
		return Results.NotFound(new { error = $"No person with {id} can be found!" });

	return Results.Ok(person);
});

app.Run();

record Person(int Id, string Name, int Age);