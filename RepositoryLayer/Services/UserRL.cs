using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly Context context;
        private readonly IConfiguration configuration;
        public UserRL(Context context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
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

        public string Login(UserLogin userLogin)
        {
            try
            { 
                var userDetails = this.context.Users.Where(e => e.Email == userLogin.Email && e.Password == EncryptPassword(userLogin.Password)).FirstOrDefault();
                if (userDetails != null)
                {
                    var result = GenerateJSONWebToken(userDetails.Email, userDetails.Id);
                    return result;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GenerateJSONWebToken(string email, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configuration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                   new Claim(ClaimTypes.Email, email),
                   new Claim("Id", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string ForgetPassword(string email)
        {
            try
            {
                var result = this.context.Users.Where(x => x.Email == email).FirstOrDefault();
                if (result != null)
                {
                    var token = GenerateJSONWebToken(result.Email, result.Id);
                    MSMQ msmq = new MSMQ();
                    msmq.sendData2Queue(token);
                    return token.ToString();
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                var result = this.context.Users.FirstOrDefault(x => x.Email == email);
                if (password.Equals(confirmPassword) && result != null)
                {
                    result.Password = EncryptPassword(password);
                    this.context.SaveChanges();
                    return true;
                }
                else
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

        
        public  string DecryptPassword(string encData)
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
