using BadgerClan.Logic;

namespace BadgerClan.Api.Strategies;

public class DoNothingStrategy : IStrategy
{
    public Task<List<Move>> GetMovesAsync(MoveRequest request)
    {
        return Task.FromResult(new List<Move>());
    }
}
