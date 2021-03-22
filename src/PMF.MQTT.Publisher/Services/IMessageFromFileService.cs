namespace PMF.MQTT.Publisher.Services
{
    using System.Threading.Tasks;
    using PMF.MQTT.Publisher.DTO;

    public interface IMessageFromFileService
    {
        Task PublishAllMessages(string folderPath);
    }
}
