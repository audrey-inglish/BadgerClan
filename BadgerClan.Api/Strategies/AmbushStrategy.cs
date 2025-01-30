using BadgerClan.Logic;

namespace BadgerClan.Api.Strategies;

public class AmbushStrategy : IStrategy
{
    // if there are 1-3 on their own, attack them
    // otherwise, move away from the nearest enemy

    private const int AttackThreshold = 3;

    public Task<List<Move>> GetMovesAsync(MoveRequest request)
    {
        var myUnits = request.Units.Where(u => u.Team == request.YourTeamId);
        var enemyUnits = request.Units.Where(u => u.Team != request.YourTeamId).ToList();

        var moves = new List<Move>();

        // loop through all my guys & decide on the move
        foreach (var unit in myUnits)
        {
            var closestEnemy = enemyUnits.OrderBy(u => u.Location.Distance(unit.Location)).FirstOrDefault();


            if (closestEnemy != null)
            {
                int enemyNeighborCount = enemyUnits.Count(e => e.Location.Distance(closestEnemy.Location) <= unit.AttackDistance);

                // check to see if the enemy is within the group size I set
                if (enemyNeighborCount <= AttackThreshold)
                {
                    // if they're in attack range, attack. otherwise, walk towards them
                    if (closestEnemy.Location.Distance(unit.Location) <= unit.AttackDistance)
                    {
                        moves.Add(new Move(MoveType.Attack, unit.Id, closestEnemy.Location));
                    }
                    else
                    {
                        moves.Add(new Move(MoveType.Walk, unit.Id, unit.Location.Toward(closestEnemy.Location)));
                    }
                }
                else
                {
                    // run away if there are too many enemies nearby
                    var retreatDirection = unit.Location.Away(closestEnemy.Location);
                    moves.Add(new Move(MoveType.Walk, unit.Id, retreatDirection));
                }
            }
        }
        return Task.FromResult(moves);
    }
}
