using BadgerClan.Logic;

namespace BadgerClan.Api.Strategies;

public class CornerRetreatStrategy : IStrategy
{
    public Task<List<Move>> GetMovesAsync(MoveRequest request)
    {
        var myUnits = request.Units.Where(u => u.Team == request.YourTeamId);
        var moves = new List<Move>();

        var targetLocation = new Coordinate(0, 0);

  

        foreach (var unit in myUnits)
        {
            moves.Add(new Move(MoveType.Walk, unit.Id, unit.Location.Toward(targetLocation)));
        }

        return Task.FromResult(moves);
    }
}
