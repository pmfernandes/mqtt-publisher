namespace PMF.MQTT.Publisher.Settings
{
    public interface IApplicationSettings
    {
        public BrokerHostSettings BrokerHostSettings { get; }

        public ClientSettings ClientSettings { get; }
    }
}
