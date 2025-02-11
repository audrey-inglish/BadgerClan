using BadgerClan.Shared;
using ProtoBuf.Grpc.Server;

namespace BadgerClan.Api;

public class GrpcStrategyService(StrategyService service) : IStrategyService
{
    public Task SetStrategy(SetStrategyRequest request)
    {
        service.SetStrategy(request.StrategyName);
        return Task.FromResult(new SetStrategyResponse
        {
            IsSuccess = true,
            Message = $"Strategy set to {request.StrategyName}"
        });
    }
}
