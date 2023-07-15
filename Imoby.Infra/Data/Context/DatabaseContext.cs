using MongoDB.Driver;

namespace Imoby.Infra.Data.Context;
public class DatabaseContext : IDatabaseContext
    {
        private MongoClient dbmsClient;
        public IMongoDatabase Database { get; set; }
        public DatabaseContext()
        {
            dbmsClient = new MongoClient(GetDatabaseConnectionString());
            Database = dbmsClient.GetDatabase("Imoby");
        }

        public string GetDatabaseConnectionString()
        {
            string response;
            var servername = Environment.GetEnvironmentVariable("SERVERNAME");
            switch (servername)
            {
                
                case "SANDBOX":
                    response = "mongodb://mongodb:27017";
                    break;
                case "DEV":
                    response = "mongodb://mongodb:27017";
                break;
                default:
                    response = "mongodb://mongodb:27017";
                break;
            }

            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine($"\n SERVERNAME: {servername} \n");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            return response;
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return Database.GetCollection<T>(typeof(T).Name);
        }
    }