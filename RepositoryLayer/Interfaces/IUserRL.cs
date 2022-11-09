using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public UserEntity Registration(UserRegistration userRegistration);
        public bool Login(UserLogin userLogin);
           
    }
}
