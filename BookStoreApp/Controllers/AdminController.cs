using BusinessLayer.interfaces;
using CommonLayer.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AdminController : ControllerBase
    {
        IConfiguration _configuration;
        IAdminBL _adminBL;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IConfiguration _configuration, IAdminBL adminBL, ILogger<AdminController> logger)
        {
            this._configuration = _configuration;
            this._adminBL = adminBL;
            this._logger = logger;
        }
        [HttpPost("loginAdmin")]
        public IActionResult loginAdmin(AdminLoginModel adminLogin)
        {
            try
            {
                var token = this._adminBL.LoginAdmin(adminLogin);
                if (token == null)
                {
                    return this.BadRequest(new { success = false, status = 401, message = "Email not found" });
                }
                this._logger.LogInformation(" user login successfully with email Id:" + adminLogin.email);
                return this.Ok(new { success = true, status = 200, token = token, message = $"login successful {adminLogin.email}" });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
