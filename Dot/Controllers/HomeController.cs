using Dot.Services;
using Dot.Services.Models;
using Dot.Models;
using Dot.Data;
using Dot.Data.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Dot.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDotService _dotService;
        private readonly IConfiguration _configuration;

        public HomeController(IDotService dotService, IConfiguration configuration)
        {
            _dotService = dotService;
            _configuration = configuration;
        }

        /// <summary>
        /// Shows list of users
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(_dotService.GetAllUsers());
        }

        public async Task<IActionResult> SearchAsync(string query)
        {
            query = $"{_configuration.GetSection("GitHubSearchUserApiBase").Value}{query}";
            string searchResults = await DOt.Helpers.Utilities.MakeGitHubApiRequestAsync(query);
            var searchedProfiles = _dotService.GetParsedUsersList(searchResults);
            return View("Index", searchedProfiles);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddUser(UserVm user)
        {
            if(user != null && user.Id > 0)
            {
                _dotService.AddUser(user);
            }

            return RedirectToAction("Error", new { Message = "Invalid user data, please contact your administrator" });
        }

        /// <summary>
        /// Allows the user to toggle favorite flag for each user
        /// </summary>
        /// <param name="id"></param>
        public void ToggleFavorite(int id)
        {
            var user = _dotService.FindUserById(id);
            if(user == null)
            {
               RedirectToAction("Error", "Index");
            }
            else
            {
                user.IsFavorite = !user.IsFavorite;
                _dotService.EditUser(user);
            }
        }

        public IActionResult Delete(int id)
        {
            if (!_dotService.DeleteUserById(id))
            {
                RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Generic error page
        /// </summary>
        /// <returns>View with error message</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
