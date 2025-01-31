using BadgerClan.Logic;
using BadgerClan.Api;
using BadgerClan.Api.Strategies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<StrategyService>();
builder.Services.AddSingleton<IStrategy, RunGunStrategy>();
builder.Services.AddSingleton<IStrategy, DoNothingStrategy>();
builder.Services.AddSingleton<IStrategy, CornerRetreatStrategy>();
builder.Services.AddSingleton<IStrategy, AmbushStrategy>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();



var client = new HttpClient();

app.MapGet("/", () => "API is running!");

app.MapGet("/setstrategy/{strategyName}", ([FromServices] StrategyService strategyService, string strategyName) =>
{
    app.Logger.LogInformation($"{strategyName} request");
    strategyService.SetStrategy(strategyName);
    return Results.Ok($"Strategy successfully set to {strategyName}.");
});


// take the move request and return moves based on chosen strategy
app.MapPost("/", async ([FromBody] MoveRequest request, [FromServices] StrategyService strategyService) =>
{
    app.Logger.LogInformation("Received move request for game {gameId} turn {turnNumber}", request.GameId, request.TurnNumber);
    var myMoves = new List<Move>();

    var currentStrategy = strategyService.GetStrategy();

    // get the strategy currently set and return the moves it chooses
    var strategy = strategyService.GetStrategy();
    var moves = await strategy.GetMovesAsync(request);

    return new MoveResponse(moves);
});

//app.Run();
app.Run();
