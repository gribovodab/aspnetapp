﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace aspnetapp
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static async Task Main(string[] args) =>
            await CreateHostBuilder(args).Build().RunAsync();


        /// <summary>
        /// Создание и настройка объекта
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenAnyIP(4999, listenOptions => 
                            listenOptions.Protocols = HttpProtocols.Http1);

                        options.ListenAnyIP(5000, listenOptions => 
                            listenOptions.Protocols = HttpProtocols.Http2);
                    })
                    .UseStartup<Startup>());
    }
}
