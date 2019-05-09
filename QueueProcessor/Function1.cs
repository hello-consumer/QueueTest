using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using QueueCore;

namespace QueueProcessor
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([QueueTrigger("one", Connection = "AzureStorage")]byte[] myQueueItem, ILogger log)
        {
            var originalMessage = BinaryQueueMessage<string>.FromBytes(myQueueItem);
            log.LogInformation($"C# Queue trigger function processed: {originalMessage.Data}");
        }
    }
}
