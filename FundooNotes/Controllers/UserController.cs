using BussinessLayer.Interfaces;
using CommonLayer.Models;
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
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Login succsessfull"});
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
    }
}

