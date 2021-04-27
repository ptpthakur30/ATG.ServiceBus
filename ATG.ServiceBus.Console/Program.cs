
namespace ATG.ServiceBus.Console
{
    using ATG.LoggingServiceBus.Implementation;
    using ATG.LoggingServiceBus.Interface;
    using Microsoft.Azure.ServiceBus;
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    class TopicsSubscription
    {
        static string ConnectionString = "Endpoint=sb://atgdemo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=TQ/eHVoZiIk+I7hR9hV7azor/FCYZnyvrr5rVkyaPko=";
        static string TopicPath = "atgdemo";
        static void Main(string[] args)
        {
            //    var subscriptionName = "subscription1";
            //    //Creating management client to manage artifacts
            //    var manager = new ManagementClient(ConnectionString);
            //    if (!manager.TopicExistsAsync(TopicPath).Result)
            //    {
            //        manager.CreateTopicAsync(TopicPath);
            //    }

            //    //Create a subscription 
            //    var description = new SubscriptionDescription(TopicPath, subscriptionName)
            //    {
            //        AutoDeleteOnIdle = TimeSpan.FromMinutes(5)
            //    };
            //    if (!manager.SubscriptionExistsAsync(TopicPath, subscriptionName).Result)
            //    {
            //        manager.CreateSubscriptionAsync(description).Wait();
            //    }

            //    //create clients
            //    var topicClient = new TopicClient(ConnectionString, TopicPath);
            //    var subscriptionClient = new SubscriptionClient(ConnectionString, TopicPath, subscriptionName);

            //    //message pump for receiving message
            //    //subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, ExceptionHandler);


            //    //var message = new Message(Encoding.UTF8.GetBytes("Audit logging 2"));
            //    //message.Label = subscriptionName;
            //    //topicClient.SendAsync(message).Wait();
            //    Console.WriteLine("Enter exit when done");
            //    while(true)
            //    {
            //        string text = Console.ReadLine();
            //        if (text.Equals("exit")) break;
            //        var auditLog = new AuditLog()
            //        {
            //            UserID = text
            //        };
            //        string messageBody = JsonConvert.SerializeObject(auditLog);
            //        Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
            //        //var chatMessage = new Message(Encoding.UTF8.GetBytes(text));
            //        //chatMessage.Label = subscriptionName;
            //        //topicClient.SendAsync(chatMessage).Wait();
            //        topicClient.SendAsync(message).Wait();
            //    }
            //    //close client
            //    topicClient.CloseAsync().Wait();
            //    subscriptionClient.CloseAsync().Wait();
            Console.WriteLine("Enter exit when done");
            ILogging<AuditLog> logging = new Logging<AuditLog>();
            while (true)
            {
                string text = Console.ReadLine();
                if (text.Equals("exit")) break;
                var auditLog = new AuditLog()
                {
                    UserID = text
                };
                logging.SendMessageToTopic(auditLog, TopicPath, "subscription1", ConnectionString);
            }
        }



        private static Task ExceptionHandler(ExceptionReceivedEventArgs arg)
        {
            return Task.CompletedTask;
        }

        private static async Task ProcessMessageAsync(Message message, CancellationToken arg2)
        {
            //todo
            // add message body to blob storage
            var text = Encoding.UTF8.GetString(message.Body);
            Console.WriteLine($"{message.Label} > {text}");
        }
    }
}
