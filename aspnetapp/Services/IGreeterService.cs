﻿using System.Threading.Tasks;
using Greet.V1;
using Grpc.Core;

namespace aspnetapp.Services
{
    /// <summary>
    /// Greeting Service
    /// </summary>
    public interface IGreeterService
    {
        /// <summary>
        /// Метод, который принимает имя запроса и отвечает сообщением Hello.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context);
    }
}