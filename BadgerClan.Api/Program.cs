using BadgerClan.Logic;
using BadgerClan.Api;

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



var client = new HttpClient();

app.MapGet("/", () => "API is running!");

app.MapPost("/test", async (StrategyRequest request) =>
{
    // for testing:
    return Results.Ok(new {Message = "Received request from game server", ReceivedStrategy = request });
});


app.Run();
