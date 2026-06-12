
using Grpc.Net.Client;
using GrpcApi;

var channel = GrpcChannel.ForAddress("https://localhost:7092");
var client = new Greeter.GreeterClient(channel);

var response = client.SayHello(new HelloRequest { Name = "Test name" });

Console.WriteLine(response.Message);
