using System;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using log4net.Repository;
using Automation_Framework.Framework.Utilities;

namespace Automation_Framework.Framework.Logging
{
    public static class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Logger));
        private static string? logFilePath { get; set; }
        private static string rootDirectory;

        static Logger()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("Framework\\Configuration\\appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            // Default configuration
            rootDirectory = Path.Combine(AppContext.BaseDirectory, Utils.GetRequiredConfig(config, "logsDirectory"));
            Assembly? entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly == null)
                throw new InvalidOperationException("Entry assembly could not be found.");

            ILoggerRepository logRepository = LogManager.GetRepository(entryAssembly);
            var logConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Framework/Logging", "log4net.config");
            XmlConfigurator.Configure(logRepository, new FileInfo(logConfigPath));

        }


        public static void SetLogFileForTest(string testName)
        {
            // Set unique log file path based on test case name
            logFilePath = Path.Combine(rootDirectory, $"{testName}-log.txt");

            // Get appenders and update file appender
            var logRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly()); ;
            var appenders = logRepository.GetAppenders();
            foreach (var appender in appenders.OfType<log4net.Appender.FileAppender>())
            {
                appender.File = logFilePath;
                appender.ActivateOptions(); // Apply the changes
            }
        }

        public static string? GetCurrentLogFilePath() => logFilePath;

        public static void Info(string message) => log.Info(message);
        public static void Warn(string message) => log.Warn(message);
        public static void Debug(string message) => log.Debug(message);
        public static void Error(string message, Exception? ex = null)
        {
            if (ex != null)
                log.Error(message, ex);
            else
                log.Error(message);
        }

    }
}
