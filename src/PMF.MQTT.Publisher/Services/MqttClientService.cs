namespace PMF.MQTT.Publisher.Services
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using MQTTnet;
    using MQTTnet.Client;
    using MQTTnet.Client.Connecting;
    using MQTTnet.Client.Disconnecting;
    using MQTTnet.Client.Options;

    public class MqttClientService : IMqttClientService
    {
        private readonly ILogger logger;
        private readonly IMqttClient mqttClient;
        private readonly IMqttClientOptions options;

        public MqttClientService(ILogger<MqttClientService> logger, IMqttClientOptions options)
        {
            this.logger = logger;
            this.options = options;
            this.mqttClient = new MqttFactory().CreateMqttClient();

            ConfigureMqttClient();
        }

        private void ConfigureMqttClient()
        {
            this.mqttClient.ConnectedHandler = this;
            this.mqttClient.DisconnectedHandler = this;
            this.mqttClient.ApplicationMessageReceivedHandler = this;
        }

        public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            this.logger.LogTrace($"Message received: {eventArgs.ApplicationMessage.Topic}");

            return Task.CompletedTask;
        }

        public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            this.logger.LogInformation("Connected");
            await this.mqttClient.SubscribeAsync("hello/world");
        }

        public Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
        {
            throw new System.NotImplementedException();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("MQTT Service Start!");

            await this.mqttClient.ConnectAsync(options);
            if (!this.mqttClient.IsConnected)
            {
                await this.mqttClient.ReconnectAsync();
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("MQTT Service Stop!");

            if (cancellationToken.IsCancellationRequested)
            {
                var disconnectOption = new MqttClientDisconnectOptions
                {
                    ReasonCode = MqttClientDisconnectReason.NormalDisconnection,
                    ReasonString = "NormalDiconnection"
                };
                await this.mqttClient.DisconnectAsync(disconnectOption, cancellationToken);
            }
            await this.mqttClient.DisconnectAsync(cancellationToken);
        }

        public Task PublishMessageAsync(MqttApplicationMessage message)
        {
            return this.mqttClient.PublishAsync(message);
        }
    }
}
