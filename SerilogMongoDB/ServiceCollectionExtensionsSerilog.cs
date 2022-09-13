using Serilog;
using Serilog.Events;

namespace WebApplication1
{
    public static class ServiceCollectionExtensionsSerilog
    {
        public static WebApplicationBuilder AddSerilogConfiguration(this WebApplicationBuilder builder)
        {

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", $"API Serilog Example - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                .WriteTo.MongoDBBson(builder.Configuration.GetSection("MongoDbSettings").GetSection("LogDB")["ConnectionString"])
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger);

            return builder;
        }
    }
}
