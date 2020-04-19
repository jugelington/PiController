using System.Reflection;
using PiController.Settings;
using PiController.ShellCommands;
using Renci.SshNet;

namespace PiController.SSH
{
    public class Client
    {
        private readonly ConnectionInfo _connectionInfo;

        public Client(AppSettings appSettings)
        {
            var sshSettings = appSettings.SshSettings;
            var keyFile = CreatePrivateKeyFile();
            _connectionInfo = CreateConnectionInfo(sshSettings.Host, sshSettings.Username, keyFile);
        }

        private PrivateKeyFile CreatePrivateKeyFile()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var rsaKeyLocation = string.Format("PiController.Settings.{0}", "id_rsa");
            var names =  Assembly.GetExecutingAssembly().GetManifestResourceNames();
            using (var stream = executingAssembly.GetManifestResourceStream(rsaKeyLocation))
            {
                return new PrivateKeyFile(stream);
            }
        }
        private ConnectionInfo CreateConnectionInfo(string host, string username, PrivateKeyFile keyFile)
        {
            return new ConnectionInfo(host, username, new PrivateKeyAuthenticationMethod(username, keyFile));
        }

        private SshClient CreateSshClient()
        {
            return new SshClient(_connectionInfo);
        }

        public void DispatchCommand(Command cmd)
        {
            using (var client = CreateSshClient())
            {
                client.Connect();
                client.RunCommand(cmd.Body);
                client.Disconnect();
            }
        }
    }
}