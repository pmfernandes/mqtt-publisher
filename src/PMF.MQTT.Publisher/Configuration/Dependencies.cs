namespace PMF.MQTT.Publisher.Configuration
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using MQTTnet.Client.Options;
    using PMF.MQTT.Publisher.Services;
    using PMF.MQTT.Publisher.Settings;

    public static class Dependencies
    {
        public static void ConfigureDependencies(
            IServiceCollection services,
            IApplicationSettings applicationSettings)
        {
            services.AddSingleton(applicationSettings);

            services.AddMqttClientHostedService(applicationSettings);

            services.AddSingleton<IMessageFromFileService, MessageFromFileService>();
        }

        private static IServiceCollection AddMqttClientHostedService(
            this IServiceCollection services,
            IApplicationSettings applicationSettings)
        {
            services.AddMqttClientServiceWithConfig(aspOptionBuilder =>
            {
                var clientSettinigs = applicationSettings.ClientSettings;
                var brokerHostSettings = applicationSettings.BrokerHostSettings;

                aspOptionBuilder
                    .WithCredentials(clientSettinigs.UserName, clientSettinigs.Password)
                    .WithClientId(clientSettinigs.Id)
                    .WithTcpServer(brokerHostSettings.Host, brokerHostSettings.Port);
            });

            return services;
        }

        private static IServiceCollection AddMqttClientServiceWithConfig(
            this IServiceCollection services,
            Action<AspCoreMqttClientOptionBuilder> configure)
        {
            services.AddSingleton<IMqttClientOptions>(serviceProvider =>
            {
                var optionBuilder = new AspCoreMqttClientOptionBuilder(serviceProvider);
                configure(optionBuilder);
                return optionBuilder.Build();
            });

            services.AddSingleton<IMqttClientService, MqttClientService>();

            return services;
        }
    }
}
