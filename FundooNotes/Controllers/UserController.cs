using System.Security.Claims;
using BussinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public IActionResult AddUser(UserRegistration userRegistration)
        {
            try
            {
                var result = this.userBL.Registration(userRegistration);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Registration succsessfull", response=result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Registration Unsuccsessfull" });

                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                var result = this.userBL.Login(userLogin);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login succsessfull", data=result});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Login Unsuccsessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            var result = this.userBL.ForgetPassword(email);
            if(result != null)
            {
                return this.Ok(new { success = true, message = "Email sent successfully" });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Email has not sent" });
            }
        }

        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword
            (string password, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = this.userBL.ResetPassword(email, password, confirmPassword);
                if(result != false)
                {
                    return this.Ok(new { success = true, message = "Your password has been reset" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Try again" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

