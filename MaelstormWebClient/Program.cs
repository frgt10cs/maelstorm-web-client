using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MaelstormWebClient.Models;
using MaelstormWebClient.Services.Implementations;
using MaelstormWebClient.Services.Abstractions;

namespace MaelstormWebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Logging.SetMinimumLevel(LogLevel.Information);

            builder.Services.AddScoped(
                sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton(
                sp => new WindowState());

            builder.Services.AddSingleton<IDialogService, DialogService>();
            builder.Services.AddSingleton<IMessagesPanelsService, MessagesPanelsService>();

            await builder.Build().RunAsync();
        }
    }
}