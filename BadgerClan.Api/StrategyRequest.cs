using BadgerClan.Logic;

namespace BadgerClan.Api;

public class StrategyRequest
{
    public int YourTeamId { get; set; }
    string StrategyType { get; set; }
    MoveRequest MoveRequest { get; set; }
}

//public class StrategyType
//{

//}
