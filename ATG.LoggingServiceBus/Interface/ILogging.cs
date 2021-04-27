namespace ATG.LoggingServiceBus.Interface
{
    public interface ILogging<T> where T : class
    {
        public void SendMessageToTopic(T audit, string topicPath, string subscriptionName, string connectionString);
    }
}
