using AutoMapper;
using Dot.Data;
using Dot.Data.Domain;
using Dot.Services.Models;
using Dot.Data;
using Dot.Data.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Dot.Services
{
    public interface IDotService
    {
        List<UserVm> GetAllUsers();

        UserVm FindUserById(int id);

        bool AddUser(UserVm user);

        bool EditUser(UserVm user);

        bool DeleteUserById(int id);

    }

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

        public bool AddUser(UserVm userVm)
        {
            try
            {
                var user = _mapper.Map<User>(userVm);
                return _dotRepository.Add(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public bool DeleteUserById(int id)
        {
            throw new NotImplementedException();
        }

        public bool EditUser(UserVm user)
        {
            throw new NotImplementedException();
        }

        public UserVm FindUserById(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserVm> GetAllUsers()
        {
            var users = _dotRepository.GetAll();
            return _mapper.Map<List<UserVm>>(users);
        }
    }
}
