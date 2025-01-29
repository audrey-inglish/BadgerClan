using BadgerClan.Logic;
using BadgerClan.Logic.Bot;

namespace BadgerClan.Api.Strategies;

public class RunGunStrategy : IStrategy
{
    public Task<List<Move>> GetMovesAsync(MoveRequest request)
    {
        var myUnits = request.Units.Where(u => u.Team == request.YourTeamId);
        var moves = new List<Move>();

        // loop through all my guys & decide on the move
        foreach (var unit in myUnits)
        {
            var enemies = request.Units.Where(u => u.Team != request.YourTeamId);
            var closest = enemies.OrderBy(u => u.Location.Distance(unit.Location)).FirstOrDefault();

            if (closest != null)
            {
                // checking to see if nearest enemy is close enough to attack
                if (closest.Location.Distance(unit.Location) <= unit.AttackDistance)
                {
                    moves.Add(new Move(MoveType.Attack, unit.Id, closest.Location));
                }
                else if (request.Medpacs > 0 && unit.Health < unit.MaxHealth)
                {
                    moves.Add(new Move(MoveType.Medpac, unit.Id, unit.Location));
                }
                else
                {
                    // if not close enough to attack, get closer to enemy
                    moves.Add(new Move(MoveType.Walk, unit.Id, unit.Location.Toward(closest.Location)));
                }
            }
        }
        return Task.FromResult(moves);
    }
}

