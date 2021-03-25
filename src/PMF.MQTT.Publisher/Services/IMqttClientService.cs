namespace PMF.MQTT.Publisher.Services
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using MQTTnet;
    using MQTTnet.Client.Connecting;
    using MQTTnet.Client.Disconnecting;
    using MQTTnet.Client.Receiving;

    public interface IMqttClientService : IHostedService,
                                          IMqttClientConnectedHandler,
                                          IMqttClientDisconnectedHandler,
                                          IMqttApplicationMessageReceivedHandler
    {
        Task PublishMessageAsync(MqttApplicationMessage message);
    }
}
