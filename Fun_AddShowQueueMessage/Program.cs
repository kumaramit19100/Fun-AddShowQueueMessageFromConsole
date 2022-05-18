using System;
using System.Threading.Tasks;
using CloudStorageAccount = Microsoft.WindowsAzure.Storage.CloudStorageAccount;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Fun_AddShowQueueMessage
{
    class Program
    {
        public static string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=akstaccount;AccountKey=Wt+DV9o6sOEyGpl68QUZKZHtNyNDoDse6E5qKUe40vFiGkh3ZCygkA1vlndos7I7lwD60iRGGJrq+AStgsHZAQ==;EndpointSuffix=core.windows.net";
        static void Main(string[] args)
        {
            Console.WriteLine("Please Choose Option for Performe Task ");
            Console.WriteLine("1. For Add Message, 2. For Show Message, 3. For Delete Message");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num == 1)
            {
                Console.WriteLine("You are Select for Add Message!\n");
                AddMessage();
            }
            if (num == 2)
            {
                Console.WriteLine("You are Select for Show Queue Message!\n");
                ShowMessage();
            }
            if (num == 3)
            {
                Console.WriteLine("You are Select for Delete Message!\n");
                DeleteMessage();
            }
            Console.ReadKey();
        }
        public static void AddMessage()
        {
            Console.WriteLine("Write Some message for add in Queue ");
            string msg = Console.ReadLine();
            CloudStorageAccount cls = CloudStorageAccount.Parse(ConnectionString);
            CloudQueueClient queueClient = cls.CreateCloudQueueClient();
            CloudQueue cloudQueue = queueClient.GetQueueReference("test");
            CloudQueueMessage queueMessage = new CloudQueueMessage(msg);
            if (queueMessage.AsString.Length > 0)
            {
                cloudQueue.AddMessage(queueMessage);
                Console.WriteLine("Message Add Successfully!!");
            }
            else
            {
                Console.WriteLine("Please write some messgae for add in Queue !");
                Console.Beep(500, 1500);
            }
        }

        public static void ShowMessage()
        {
            Console.WriteLine("Your Queue Message is : ");
            CloudStorageAccount cls = CloudStorageAccount.Parse(ConnectionString);
            CloudQueueClient queueClient = cls.CreateCloudQueueClient();
            CloudQueue cloudQueue = queueClient.GetQueueReference("test");
            CloudQueueMessage queueMessage = cloudQueue.GetMessage();
            if (queueMessage.AsString.Length > 0)
            {
                Console.WriteLine(queueMessage.AsString);
            }   
        }

        public static void DeleteMessage()
        {
            Console.WriteLine("This Message is Deleted from Queue : ");
            CloudStorageAccount cls = CloudStorageAccount.Parse(ConnectionString);
            CloudQueueClient queueClient = cls.CreateCloudQueueClient();
            CloudQueue cloudQueue = queueClient.GetQueueReference("test");
            CloudQueueMessage queueMessage = cloudQueue.GetMessage();          
            if (queueMessage.AsString.Length > 0)
            {
                Console.WriteLine(queueMessage.AsString);
                cloudQueue.DeleteMessage(queueMessage.Id,queueMessage.PopReceipt);
                Console.WriteLine("\nYour Message is Deleted!!!");
            }            
        }
    }
}
