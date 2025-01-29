using BadgerClan.Logic;

namespace BadgerClan.Api.Strategies;

public interface IStrategy
{
    public Task<List<Move>> GetMovesAsync(MoveRequest request);
}
