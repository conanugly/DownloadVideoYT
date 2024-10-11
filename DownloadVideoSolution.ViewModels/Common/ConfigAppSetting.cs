using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadVideoSolution.ViewModels.Common
{
    public static class ConfigAppSetting
    {
        public static Appsetings Appsetings { get; set; }
        public static Tokens Token { get; set; }
        public static ConnectionStrings ConnectionString { get; set; }
    }

    public class Appsetings
    {
        public string? OutputDirectory { get; set; }
    }

    public class Tokens
    {
        public string? Key {  get; set; }
        public string? Issuer { get; set; }
    }

    public class ConnectionStrings
    {
        public string? DefaultConnection { get; set; }
    }
}
