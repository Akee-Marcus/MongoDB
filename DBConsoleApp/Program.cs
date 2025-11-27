using MongoDBConnector;
using System;
using System.Data;

class Program
{
    static async Task Main()
    {
        while (true)
        {
            Console.WriteLine("Choose DB:");
            Console.WriteLine("1. MongoDB");
            Console.WriteLine("2. PostgreSQL");
            Console.WriteLine("0. Quit");

            var choice = Console.ReadLine();
            if (choice?.ToUpper() == "0")
                break;

            Console.Write("Enter connection string: ");
            var connString = Console.ReadLine();

            IDBConnector? connector = choice switch
            {
                "1" => new MongoDbService(connString),
                "2" => new PostgresConnector(connString),
                _ => null
            };

            if (connector == null)
            {
                Console.WriteLine("Invalid choice.");
                continue;
            }

            Console.WriteLine("Pinging database...");
            bool result = await connector.PingAsync();
            Console.WriteLine(result ? "Success!" : "Failed.");
        }
    }
}
