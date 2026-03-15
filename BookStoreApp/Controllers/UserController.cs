using BusinessLayer.interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        readonly IConfiguration _configuration;
       readonly IUserBL _userBL;
        private readonly ILogger<UserController> _logger;
        public UserController(IConfiguration configuration, IUserBL userBL, ILogger<UserController> logger)
        {
            this._configuration = configuration;
            _userBL = userBL;
            this._logger = logger;
        }
        [HttpPost("registerUser")]
        public IActionResult registerUser(RegisterModel registerModel)
        {
            try
            {
               this._userBL.RegisterUser(registerModel);
                this._logger.LogInformation("New user registered successfully with email Id:" + registerModel.email);
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
                    this._logger.LogInformation(" user login successfully with email Id:" + login.email);
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
                    this._logger.LogInformation("forget password has been sent successfully to email Id:" + email + "\n");
                    return this.Ok(new { success = true, status = 200, message = $"Reset password link has been sent to {email}" });
                }
                return this.BadRequest(new { success = false, status = 401, message = "Wrong email" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ResetPassword/{email}")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel, string email)
        {
            try
            {
                if(resetPasswordModel.Newpassword != resetPasswordModel.ConfirmNewPassword)
                {
                    return this.BadRequest(new { success = false, status = 401, message = "new password and confirm new password are not same" });
                }
                var res = this._userBL.ResetPassword(resetPasswordModel,email);
                if (res == true)
                {
                    this._logger.LogInformation(" password has been changed successfully");
                    return this.Ok(new { success = true, status = 200, message ="Password has been changed successfully"});
                }
                return this.BadRequest(new { success = false, status = 401, message = "wrong password" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
