namespace PMF.MQTT.Publisher.Hosting
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using PMF.MQTT.Publisher.Services;

    public class ConsoleHostedService : IHostedService
    {
        private readonly IMqttClientService mqttClientService;

        public ConsoleHostedService(IMqttClientService executeService)
        {
            this.mqttClientService = executeService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await this.mqttClientService.StartAsync(cancellationToken);

            return;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await this.mqttClientService.StopAsync(cancellationToken);

            return;
        }
    }
}
