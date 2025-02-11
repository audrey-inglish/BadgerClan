using BadgerClan.Shared;
using ProtoBuf.Grpc.Server;

namespace BadgerClan.Api;

public class GrpcStrategyService : IStrategyService
{
    public Task SetStrategy(SetStrategyRequest request)
    {
        return Task.FromResult(new SetStrategyResponse
        {
            IsSuccess = true,
            Message = $"Strategy set to {request.StrategyName}"
        });
    }
}
