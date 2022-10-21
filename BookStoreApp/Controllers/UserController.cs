using BusinessLayer.interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        readonly IConfiguration _configuration;
       readonly IUserBL _userBL;
        public UserController(IConfiguration configuration, IUserBL userBL)
        {
            this._configuration = configuration;
            _userBL = userBL;
        }
        [HttpPost("registerUser")]
        public IActionResult registerUser(RegisterModel registerModel)
        {
            try
            {
               this._userBL.RegisterUser(registerModel);
               return this.Ok(new { success = true, status = 200, message = $"Registration successful for {registerModel.email}" });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("LoginUser")]
        public IActionResult LoginUser(loginModel login)
        {
            try
            {
                var token = this._userBL.LoginUser(login);
                if (token == null)
                {
                    return this.BadRequest(new { success = false, status = 401, message = "Email not found" });
                }
                return this.Ok(new { success = true, status = 200,token= token, message = $"login successful {login.email}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("ForgotPassword/{email}")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var res = this._userBL.ForgotPassword(email);
                if (res==true)
                {
                    return this.Ok(new { success = true, status = 200, message = $"Reset password link has been sent to {email}" });
                }
                return this.BadRequest(new { success = false, status = 401, message = "Wrong email" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
