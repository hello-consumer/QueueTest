using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Extensions.Configuration;
using QueueCore;

namespace QueueTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("AzureStorage");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);


            CloudQueueClient cloudQueueClient = storageAccount.CreateCloudQueueClient();
            string messageQueueName = configuration["Queue:Name"];
            var queue = cloudQueueClient.GetQueueReference(messageQueueName);
            queue.CreateIfNotExists();

            var queueMessage = new BinaryQueueMessage<string> { Data = args.Length > 0 ? args[0] : "" };
            var message = new CloudQueueMessage(queueMessage.ToBytes());
            queue.AddMessage(message);
        }
    }
}
