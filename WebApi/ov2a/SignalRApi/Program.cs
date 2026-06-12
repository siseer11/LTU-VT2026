using SignalRApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials()
					.SetIsOriginAllowed(_ => true);
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseCors();
app.MapHub<ChatHub>("/chat");

app.UseHttpsRedirection();
app.Run();