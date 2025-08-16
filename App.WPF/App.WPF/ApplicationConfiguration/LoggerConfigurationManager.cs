using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace MyApp.WPF.ApplicationConfiguration
{
    public static class LoggerConfigurator
    {
        public static void ConfigureLogger(string connectionString)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                    connectionString,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        AutoCreateSqlTable = true
                    },
                    restrictedToMinimumLevel: LogEventLevel.Information
                )
                .CreateLogger();


        }
    }

}
