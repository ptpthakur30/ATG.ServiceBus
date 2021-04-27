using ATG.LoggingServiceBus.Interface;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using Newtonsoft.Json;
using System.Text;

namespace ATG.LoggingServiceBus.Implementation
{
    public class Logging<T> : ILogging<T> where T : class
    {
        public void SendMessageToTopic(T audit, string topicPath, string subscriptionName, string connectionString)
        {
            //Creating management client to manage artifacts
            var manager = new ManagementClient(connectionString);
            if (!manager.TopicExistsAsync(topicPath).Result)
            {
                manager.CreateTopicAsync(topicPath);
            }

            //Create a subscription 
            var description = new SubscriptionDescription(topicPath, subscriptionName);
            //{
            //    AutoDeleteOnIdle = TimeSpan.FromMinutes(5)
            //};

            if (!manager.SubscriptionExistsAsync(topicPath, subscriptionName).Result)
            {
                manager.CreateSubscriptionAsync(description).Wait();
            }

            //create clients
            var topicClient = new TopicClient(connectionString, topicPath);

            //create and sending message to topic
            string messageBody = JsonConvert.SerializeObject(audit);
            Message message = new Message(Encoding.UTF8.GetBytes(messageBody))
            {
                Label = "AuditLog",
                ContentType = "application/json",
            };

            topicClient.SendAsync(message).Wait();

            //close client
            topicClient.CloseAsync().Wait();

        }
    }
}
