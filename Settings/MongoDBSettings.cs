using System;
namespace catalog.Settings
{
    public class MongoDBSettings
    {
        public static string name = "MongoDBSettings";

        public string Host { get; set; }
        public int Port { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public string ConnectionString
        {
            get
            {
                //mongodb://mongoadmin:Pass%23word1@localhost:27017/?authSource=admin&readPreference=primary&appname=mongodb-vscode%200.6.14&ssl=false
                return $"mongodb://{Username}:{Password.Replace("#","%23")}@{Host}:{Port}";
            }
        }
    }
}
