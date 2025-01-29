using BadgerClan.Api.Strategies;

namespace BadgerClan.Api;

public class StrategyService
{
    private readonly Dictionary<string, IStrategy> _allStrategies;
    private IStrategy _currentStrategy;

    public StrategyService(IEnumerable<IStrategy> strategies)
    {
        _allStrategies = strategies.ToDictionary(s => s.GetType().Name.ToLower().Replace("strategy", "")); ;

        // default strategy is Run & Gun
        _currentStrategy = _allStrategies["rungun"];
    }

    public IStrategy GetStrategy()
    {
        return _currentStrategy;
    }

    public void SetStrategy(string requestedStrategy)
    {
        if (_allStrategies.TryGetValue(requestedStrategy.ToLower(), out var strategy))
        {
            _currentStrategy = strategy;
        }
    }
}
