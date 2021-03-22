namespace PMF.MQTT.Publisher.Hosting
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using PMF.MQTT.Publisher.Services;

    public class ConsoleHostedService : IHostedService
    {
        private readonly IMqttClientService mqttClientService;
        private readonly IMessageFromFileService messageFromFileService;

        public ConsoleHostedService(
            IMqttClientService executeService,
            IMessageFromFileService messageFromFileService)
        {
            this.mqttClientService = executeService;
            this.messageFromFileService = messageFromFileService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await this.mqttClientService.StartAsync(cancellationToken);

            await this.messageFromFileService.PublishAllMessages($"{Directory.GetCurrentDirectory()}\\MessageSamples");

            return;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await this.mqttClientService.StopAsync(cancellationToken);

            return;
        }
    }
}
