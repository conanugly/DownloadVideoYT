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
        public static ConnectionStrings ConnectionString { get; set; }
        public static Authentication Authentications { get; set; }
    }

    public class Appsetings
    {
        public string? OutputDirectory { get; set; }
    }

    public class Authentication
    {
        public AuthenticationJwt Jwt { get; set; }
        public AuthenticationClients Clients { get; set; }
    }

    public class ConnectionStrings
    {
        public string? DefaultConnection { get; set; }
    }

    public class AuthenticationClients
    {
        public string Id { get; set; }
        public string Secret { get; set; }
    }

    public class AuthenticationJwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int? TimeoutInMinute { get; set; }
    }
}
