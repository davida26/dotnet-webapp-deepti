using Dot.Data;
using Dot.Data;
using Dot.Data.Domain;
using DOt.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dot
{
    public partial class Startup
    {

        /// <summary>
        /// Seed the database with data from the GitHub API if there are no users in the database already
        /// </summary>
        /// <returns></returns>
        private static async Task<List<User>> InitializeAppDataAsync(string gitHubUrl, bool returnFollowers, bool isLive, string sampleUsers)
        {
            try
            {
                var result = string.Empty;
                if (!isLive)
                {
                    result = DemoData.GetRandomUsers(sampleUsers);
                }
                else
                {
                    result = await Utilities.MakeGitHubApiRequestAsync(gitHubUrl);
                }

                if (!string.IsNullOrEmpty(result))
                {
                    var parsedUsers = JsonConvert.DeserializeObject<List<User>>(result);

                    if (parsedUsers != null)
                    {
                        if (returnFollowers)
                        {
                            return parsedUsers;
                        }
                        else
                        {
                            foreach (var user in parsedUsers)
                            {
                                if (!string.IsNullOrEmpty(user.Followers_Url))
                                {
                                    // fetch login data for all followers
                                    var followers = InitializeAppDataAsync(user.Followers_Url, true, isLive, sampleUsers).Result;
                                    user.Followers.AddRange(followers.Select(f => new Follower { Login = f.Login }));
                                }
                            }

                            await PersistData(parsedUsers);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var err = ex;
            }

            return await Task.FromResult(new List<User>());
        }

        /// <summary>
        /// Stores all data fetched from the remote api or file
        /// </summary>
        /// <param name="parsedUsers"></param>
        /// <returns>none</returns>
        private static async Task PersistData(List<User> parsedUsers)
        {
            var options = new DbContextOptionsBuilder<DotContext>()
                .UseInMemoryDatabase(databaseName: "DotDatabase")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

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
                        uow.Save();
                    }
                }
            }
        }
    }
}
