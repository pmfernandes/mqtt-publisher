namespace PMF.MQTT.Publisher.DTO
{
    public class MessageToPublish
    {
        public string Topic { get; set; }

        public string Payload { get; set; }

        public bool Retain { get; set; }
    }
}
