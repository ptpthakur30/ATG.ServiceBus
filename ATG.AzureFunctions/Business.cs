using Microsoft.Azure.Cosmos;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ATG.AzureFunctions
{
    public class Business
    {
        public static async Task CreateDocument(dynamic auditlog)
        {
            var container = Shared.Client.GetContainer(Environment.GetEnvironmentVariable("loggingDB"), Environment.GetEnvironmentVariable("loggingContainer"));
            var userID = string.Format("{0}_{1}", "ATG", auditlog.UserID);
            dynamic document1Dynamic = new
            {
                id = Guid.NewGuid(),
                UserID = userID
            };
            await container.CreateItemAsync(document1Dynamic, new PartitionKey(userID));
        }
    }
}
