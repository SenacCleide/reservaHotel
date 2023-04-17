using Autofac.Configuration;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Hotel.WebApi;
using Autofac.Extensions.DependencyInjection;
using Autofac.Core.Activators.Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureAppConfiguration((builderContext, config) =>
            {
                IWebHostEnvironment env = (IWebHostEnvironment)builderContext.HostingEnvironment;
                if (env.IsDevelopment())
                {
                    config.AddJsonFile("autofacHlg.json");
                }
                //else if (env.IsProduction())
                //{
                //    config.AddJsonFile("autofacPro.json");
                //}
                config.AddEnvironmentVariables();
            })
            .ConfigureServices(services => services.AddAutofac());
}
