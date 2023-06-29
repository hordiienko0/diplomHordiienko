namespace Ctor.Application.Common.Models;

public class MessageBrokerSettings
{
    public MessageBrokerSettings(string hostName, ushort port, string userName, string password)
    {
        HostName = hostName;
        Port = port;
        UserName = userName;
        Password = password;
    }

    public string HostName { get; set; }
    public ushort Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}