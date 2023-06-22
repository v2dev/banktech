using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using System;
using Microsoft.AspNetCore.Server.IISIntegration;

public partial class Program
{
    private static IConfiguration _configuration;

    public static IConfiguration Configuration => _configuration;

    static void Main(string[] args)
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddServiceModelServices();
        builder.Services.AddServiceModelMetadata();
        builder.Services.AddAuthentication();


        var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
               
        builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();
       
        var app = builder.Build();

        app.UseServiceModel(serviceBuilder =>
        {
            serviceBuilder.AddService<Service>();
            //serviceBuilder.AddServiceEndpoint<Service, IJwtToken>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), "/Service.svc");
            serviceBuilder.AddServiceEndpoint<Service, IJwtToken>(new BasicHttpBinding(),"/Service.svc");
            var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
            serviceMetadataBehavior.HttpGetEnabled = true;
        });       

        app.Run();

    }
}
