using BadgerClan.Logic;

namespace BadgerClan.Api.Strategies;

public class JustWalkStrategy : IStrategy
{
    public Task<List<Move>> GetMovesAsync(MoveRequest request)
    {
        var myUnits = request.Units.Where(u => u.Team == request.YourTeamId);
        var moves = new List<Move>();

        var targetLocation = new Coordinate(50, 50); // Example target location

  

        foreach (var unit in myUnits)
        {
            moves.Add(new Move(MoveType.Walk, unit.Id, targetLocation));
        }

        return Task.FromResult(moves);
    }
}
