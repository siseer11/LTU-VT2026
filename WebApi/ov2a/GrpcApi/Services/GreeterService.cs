using Grpc.Core;

namespace GrpcApi.Services;

public class GreeterService(ILogger<GreeterService> logger) : GrpcApi.Greeter.GreeterBase
{
	public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
	{
		logger.LogInformation("The message is received from {Name}", request.Name);

		return Task.FromResult(new HelloReply
		{
			Message = "Hello " + request.Name
		});
	}
}
