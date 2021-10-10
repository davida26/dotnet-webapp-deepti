using AutoMapper;
using Dot.Data;
using Dot.Data.Domain;
using Dot.Services.Models;
using Dot.Services.Models;
using Dot.Data;
using Dot.Data.Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Dot.Services
{
    /// <summary>
    /// Service to perform operations on domain objects
    /// </summary>
    public interface IDotService
    {
        UserListVm GetAllUsers();

        UserVm FindUserById(int id);

        bool AddUser(UserVm user);

        bool EditUser(UserVm user);

        bool DeleteUserById(int id);
        UserListVm GetParsedUsersList(string searchResults);
    }

    /// <summary>
    /// Implementation of the service layer
    /// </summary>
    public class DotService : IDotService
    {
        IDotRepository _dotRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public DotService(IDotRepository dotRepository, IMapper mapper, ILogger logger)
        {
            _dotRepository = dotRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Adds or updates a user entity
        /// </summary>
        /// <param name="userVm"></param>
        /// <returns>True or False to indicate success of operation</returns>
        public bool AddUser(UserVm userVm)
        {
            try
            {
                var user = _mapper.Map<User>(userVm);
                var existingUser = _dotRepository.Get(userVm.Id);
                if(existingUser != null)
                {
                   return _dotRepository.Update(user);
                }

                return _dotRepository.Add(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Removes a user entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the user was removed, false if there was an error</returns>
        public bool DeleteUserById(int id)
        {
            return _dotRepository.Delete(id);
        }

        /// <summary>
        /// Updates a user entity
        /// </summary>
        /// <param name="user"></param>
        /// <returns>True if the user was updated, false if there was an error</returns>
        public bool EditUser(UserVm user)
        {
            return _dotRepository.Update(_mapper.Map<User>(user));
        }

        /// <summary>
        /// Gets a single user entity searched by the id feild
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The user entity or null if the user was not found</returns>
        public UserVm FindUserById(int id)
        {
            var user = _dotRepository.Get(id);
            return _mapper.Map<UserVm>(user);
        }

        /// <summary>
        /// Gets a list of users
        /// </summary>
        /// <returns>A list of all users</returns>
        public UserListVm GetAllUsers()
        {
            var userslistVm = new UserListVm();
            try
            {
                userslistVm.Users.AddRange(_mapper.Map<List<UserVm>>(_dotRepository.GetAll()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return userslistVm;
        }

        public UserListVm GetParsedUsersList(string searchResults)
        {
            var userslistVm = new UserListVm();
            try
            {
                var parsedUsers = JsonConvert.DeserializeObject<SearchResults>(searchResults);
                userslistVm.Users.AddRange(_mapper.Map<List<UserVm>>(parsedUsers.Items));
                userslistVm.IsSearchResult = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return userslistVm;
        }
    }
}
