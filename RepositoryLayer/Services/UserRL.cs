using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly Context context;
        public UserRL(Context context)
        {
            this.context = context;
        }

        public UserEntity Registration(UserRegistration userRegistration)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistration.FirstName;
                userEntity.LastName = userRegistration.LastName;
                userEntity.Email = userRegistration.Email;
                userEntity.Password = userRegistration.Password;
                this.context.Users.Add(userEntity);
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Login(UserLogin userLogin)
        {
            try
            {
                var userDetails = this.context.Users.Where(e => e.Email == userLogin.Email && e.Password == userLogin.Password).FirstOrDefault();
                if (userDetails != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
