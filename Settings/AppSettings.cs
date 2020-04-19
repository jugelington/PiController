using System.Collections.Generic;

namespace PiController.Settings
{
    public class AppSettings
    {
        public SshSettings SshSettings { get; set; } 
        public IList<string> SystemServices { get; set; }
    }

    public class SshSettings
    {
        public string Host { get; set; }
        public string Username { get; set; }
    }
}
