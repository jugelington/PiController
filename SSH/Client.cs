using System.Reflection;
using PiController.Settings;
using Renci.SshNet;

namespace PiController.SSH
{
    public class Client
    {
        private readonly string _host;
        private readonly string _username;
        private readonly PrivateKeyFile _keyFile;

        public Client(AppSettings appSettings)
        {
            _host = appSettings.SshSettings.Host;
            _username = appSettings.SshSettings.Username;
            _keyFile = CreatePrivateKeyFile();
        }

        private PrivateKeyFile CreatePrivateKeyFile()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var rsaKeyLocation = string.Format("PiController.Settings.{0}", "id_rsa");

            using (var stream = executingAssembly.GetManifestResourceStream(rsaKeyLocation))
            {
                return new PrivateKeyFile(stream);
            }
        }

        public SshCommand DispatchCommand(string rawCommand)
        {
            using (var client = new SshClient(_host, _username, _keyFile))
            {
                client.Connect();
                var cmd = client.RunCommand(rawCommand);
                client.Disconnect();
                return cmd;
            }
        }
    }
}
