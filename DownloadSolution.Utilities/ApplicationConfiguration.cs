

using Microsoft.Extensions.Configuration;
using Serilog;

namespace DownloadSolution.Utilities
{
    internal class ApplicationConfiguration
    {
        private static IConfiguration _configuration;
        private static ILogger _logger;
        static ApplicationConfiguration()
        {
            var buider = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = buider.Build();

            _logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(_configuration)
                 .CreateLogger();
        }

        public static ILogger GetLogger()
        {
            return _logger;
        }

        public static string GetSetting(string key)
        {
            return _configuration[key] ?? string.Empty;
        }
    }
}
