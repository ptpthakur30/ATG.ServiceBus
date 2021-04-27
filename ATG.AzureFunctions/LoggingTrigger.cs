using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ATG.AzureFunctions
{
    public static class LoggingTrigger
    {
        [FunctionName("ATGLoggingTrigger")]
        public static void Run([ServiceBusTrigger("%loggingTopic%", "%loggingSubscription%", Connection = "connectionString")]Message mySbMsg, ILogger log)
        {
            dynamic auditLog = JsonConvert.DeserializeObject<dynamic>(Encoding.UTF8.GetString(mySbMsg.Body));
            Business.CreateDocument(auditLog).Wait();
            log.LogInformation($"{auditLog.UserID} pushed to CosmosDB");
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
