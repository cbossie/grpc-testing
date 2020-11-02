using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GRPClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            // The port number(5001) must match the port of the gRPC server.
            //using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //using var channel = GrpcChannel.ForAddress("http://54.175.211.168:5000");
            using var channel = GrpcChannel.ForAddress("https://grpc-498018814.us-east-1.elb.amazonaws.com:5000", new GrpcChannelOptions { HttpHandler = httpHandler });
            
            var client = new Greeter.GreeterClient(channel);

           

            for (int i = 0; i < 10000; i++)
            {
                var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
                Console.WriteLine($"Greeting: {reply.Message} {i}");
            }

            

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
