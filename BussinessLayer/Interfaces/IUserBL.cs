using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entities;

namespace BussinessLayer.Interfaces
{
    public interface IUserBL
    {
        public UserEntity Registration(UserRegistration userRegistration);
        public string Login(UserLogin userLogin);

    }
}
