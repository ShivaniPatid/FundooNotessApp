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
                userEntity.Password = EncryptPassword(userRegistration.Password);
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
                var userDetails = this.context.Users.Where(e => e.Email == userLogin.Email && e.Password == EncryptPassword(userLogin.Password)).FirstOrDefault();
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

        
        public static string EncryptPassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encData = Convert.ToBase64String(encData_byte);
                return encData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public static string DecryptPassword(string encData)
        { 
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            System.Text.Decoder decoder = encoding.GetDecoder();
            byte[] decode_byte = Convert.FromBase64String(encData);
            int charCount = decoder.GetCharCount(decode_byte, 0, decode_byte.Length);
            char[] decoded_Char = new char[charCount];
            decoder.GetChars(decode_byte, 0, decode_byte.Length, decoded_Char, 0);
            string result = new string(decoded_Char);
            return result; 
        }
    }
}
