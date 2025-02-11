using System.Runtime.Serialization;
using System.ServiceModel;
namespace BadgerClan.Shared;

[DataContract]
public class SetStrategyRequest
{
    [DataMember(Order = 1)]
    public string StrategyName { get; set; }
}

[DataContract]
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
    Task<SetStrategyResponse> SetStrategy(SetStrategyRequest request);
}