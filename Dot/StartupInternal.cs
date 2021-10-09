using Dot.Data;
using Dot.Data;
using Dot.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dot
{
    public partial class Startup
    {
        private const string gitHubUsersApi = "https://api.github.com/users";

        /// <summary>
        /// Seed the database with data from the GitHub API if there are no users in the database already
        /// </summary>
        /// <returns></returns>
        private static async Task InitializeAppDataAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                    var result = await client.GetStringAsync(gitHubUsersApi);

                    if (!string.IsNullOrEmpty(result))
                    {
                        var parsedUsers = JsonConvert.DeserializeObject<List<User>>(result);
                        if (parsedUsers != null)
                        {
                            var options = new DbContextOptionsBuilder<DotContext>().UseInMemoryDatabase(databaseName: "DotDatabase").Options;
                            using (var db = new DotContext(options))
                            {
                                if (await db.User.AnyAsync())
                                {
                                    return;
                                }
                                else
                                {
                                    var uow = new DotUoW(db);

                                    foreach (var user in parsedUsers)
                                    {
                                        db.User.Add(user);
                                    }

                                    uow.Save();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
