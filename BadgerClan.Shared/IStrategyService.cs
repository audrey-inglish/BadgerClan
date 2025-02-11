using System.Runtime.Serialization;
using System.ServiceModel;
namespace BadgerClan.Shared;

public class SetStrategyRequest
{
    [DataMember(Order = 1)]
    public string StrategyName { get; set; }
}

public class SetStrategyResponse
{
    [DataMember(Order = 1)]
    public bool IsSuccess { get; set; }

    [DataMember(Order = 2)]
    public string Message { get; set; }
}


[ServiceContract]
public interface IStrategyService
{
    [OperationContract]
    Task SetStrategy(SetStrategyRequest request);
}