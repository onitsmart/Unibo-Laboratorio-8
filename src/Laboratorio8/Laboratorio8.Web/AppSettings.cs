using System.Collections.Generic;

namespace Laboratorio8.Web
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string UrlBase { get; set; }

        public IEnumerable<string> ErrorContactEmails { get; set; }
        public string ChromeExecutablePath { get; set; }
    }
}
