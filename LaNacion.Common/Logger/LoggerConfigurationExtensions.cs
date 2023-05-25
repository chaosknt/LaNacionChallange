using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace LaNacion.Common.Logger
{
    public static class LoggerConfigurationExtensions
    {
        public static void SetupLoggerConfiguration(string appName, BuildInfo buildInfo, string outputPath)
        {
            Log.Logger = new LoggerConfiguration()
                 .WriteTo.File(outputPath, rollingInterval: RollingInterval.Day)
                .ConfigureBaseLogging(appName, buildInfo)
                .CreateLogger();
        }

        public static LoggerConfiguration ConfigureBaseLogging(
           this LoggerConfiguration loggerConfiguration,
           string appName,
           BuildInfo buildInfo)
        {
            loggerConfiguration
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Async(a => a.Console(theme: AnsiConsoleTheme.Code))
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithProperty(nameof(buildInfo.BuildId), buildInfo.BuildId)
                .Enrich.WithProperty(nameof(buildInfo.BuildNumber), buildInfo.BuildNumber)
                .Enrich.WithProperty(nameof(buildInfo.BranchName), buildInfo.BranchName)
                .Enrich.WithProperty(nameof(buildInfo.Info), buildInfo.Info)
                .Enrich.WithProperty("ApplicationName", appName)
                .Enrich.WithProperty("TraceCorrelation", Guid.NewGuid());

            return loggerConfiguration;
        }

    }
}
