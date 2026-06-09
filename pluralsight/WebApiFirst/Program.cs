using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers(options =>
{
	options.ReturnHttpNotAcceptable = true; // will reurn an error if any other format than the supported one is requested. By default it will return json for every type of request
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

// builder.Services.AddProblemDetails(options =>
// {
// 	options.CustomizeProblemDetails = ctx =>
// 	{
// 		ctx.ProblemDetails.Extensions.Add("errorMsg", "Stop using it wrong");
// 		ctx.ProblemDetails.Extensions.Add("server", Environment.MachineName);
// 	};
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();

	app.MapScalarApiReference();

	app.UseSwaggerUI(options =>
		{
			options.SwaggerEndpoint("/openapi/v1.json", "API v1");
		});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
