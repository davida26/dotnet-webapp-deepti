using Dot.Data.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DOt.Helpers
{
    public static class DemoData
    {
        /// <summary>
        /// Gets a string of users locally stored in a file from GitHub
        /// </summary>
        /// <returns>List of users as string</returns>
        public static string GetRandomUsersString()
        {
            var sampleUsers = ReadFile();

            List<User> sample = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(sampleUsers);

            var rand = new Random().Next(10, 60);

            var users = sample.Take(rand).ToList();

            return System.Text.Json.JsonSerializer.Serialize(users);
        }

        /// <summary>
        /// Read the file with sample data
        /// </summary>
        /// <returns>All text in the sample file</returns>
        public static string ReadFile()
        {
            var textData = Task.Run(async () =>
            {
                return await File.ReadAllTextAsync("SampleUsers.txt");
            }).GetAwaiter().GetResult();

            return textData;
        }

        /// <summary>
        /// Gets a string of users locally stored in a file from GitHub
        /// </summary>
        /// <returns>All sample users stored in the file</returns>
        public static string GetAllSampleUsers()
        {
            return ReadFile();
        }

        /// <summary>
        /// Gets a list of users in string format from the input string
        /// </summary>
        /// <param name="sampleUsers"></param>
        /// <returns>List of users in string format</returns>
        public static string GetRandomUsers(string sampleUsers)
        {
            string result;
            List<User> sample = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(sampleUsers);
            var rand = new Random().Next(10, 60);
            var users = sample.Take(rand).ToList();
            result = System.Text.Json.JsonSerializer.Serialize(users);
            return result;
        }
    }
}
