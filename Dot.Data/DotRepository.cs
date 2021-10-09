﻿using Dot.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dot.Data
{
    public interface IDotRepository
    {
        List<User> GetAll();

        User Get(int id);

        bool Add(User user);

        bool Update(User user);

        bool Delete(int id);
    }

    public class DotRepository : IDotRepository
    {
        private readonly IUoW _uoW;
        private readonly DotContext _context;
        public DotRepository(DotContext dotEntities, IUoW uoW)
        {
            _uoW = uoW;
            _context = dotEntities;
        }

        /// <summary>
        /// Adds a user entity to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>True if the new user was added to the database, false otherwise</returns>
        public bool Add(User user)
        {
            var entityState = _context.Add(user);
            _uoW.Save();
            return entityState.State == EntityState.Added;
        }

        /// <summary>
        /// Removes a user whose id matches the parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the user entity was removed, false otherwise</returns>
        public bool Delete(int id)
        {
            var targetUser = _context.User.SingleOrDefault(u => u.Id == id);
            if(targetUser != null)
            {
                _context.Favorite.RemoveRange(targetUser.Favorites);
                var entityState = _context.User.Remove(targetUser);
                _uoW.Save();
                return entityState.State == EntityState.Deleted;
            }
            return false;
        }

        /// <summary>
        /// Gets a single user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A user whose Id matches the parameter if found, null otherwise</returns>
        public User Get(int id)
        {
            return _context.User.Include(u => u.Favorites).SingleOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Gets a list of all users
        /// </summary>
        /// <returns>A list of users</returns>
        public List<User> GetAll()
        {
            return _context.User.Include(u => u.Favorites).ToList();
        }

        /// <summary>
        /// Updates the user record with new data
        /// </summary>
        /// <param name="user"></param>
        /// <returns>True if entity was modified, false otherwise</returns>
        public bool Update(User user)
        {
            var targetUser = _context.User.SingleOrDefault(u => u.Id == user.Id);
            if(targetUser != null)
            {
                targetUser.Type = user.Type;
                targetUser.Url = user.Url;
                targetUser.AvatarUrl = user.AvatarUrl;
                targetUser.Login = user.Login;

                var entityState = _context.Update(targetUser);
                _uoW.Save();
                return entityState.State == EntityState.Modified;
            }

            return false;
        }

    }
}
