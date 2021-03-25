namespace PMF.MQTT.Publisher.Services
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using PMF.MQTT.Publisher.DTO;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class MessageFromFileService : IMessageFromFileService
    {
        private readonly ILogger<MessageFromFileService> logger;
        private readonly IMqttClientService mqttClientService;

        public MessageFromFileService(
            ILogger<MessageFromFileService> logger,
            IMqttClientService mqttClientService)
        {
            this.logger = logger;
            this.mqttClientService = mqttClientService;
        }

        public async Task PublishAllMessages(string folderPath)
        {
            foreach (var filePath in Directory.GetFiles(folderPath, "*.yaml"))
            {
                try
                {
                    var messageToPublish = await DeSerializeFile(filePath);

                    this.logger.LogInformation($"Publishing message: {messageToPublish.Topic}");

                    await this.mqttClientService.PublishMessageAsync(new MQTTnet.MqttApplicationMessage
                    {
                        Topic = messageToPublish.Topic,
                        Retain = messageToPublish.Retain,
                        Payload = Encoding.UTF8.GetBytes(messageToPublish.Payload)
                    });
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, "Error DeSerializing!");
                }
            }
        }

        private static async Task<MessageToPublish> DeSerializeFile(string filePath)
        {
            var fileContent = await File.ReadAllTextAsync(filePath);

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var result = deserializer.Deserialize<MessageToPublish>(fileContent);

            return result;
        }
    }
}
