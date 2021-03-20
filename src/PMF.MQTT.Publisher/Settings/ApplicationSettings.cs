namespace PMF.MQTT.Publisher.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        public BrokerHostSettings BrokerHostSettings { get; set; }

        public ClientSettings ClientSettings { get; set; }
    }
}
