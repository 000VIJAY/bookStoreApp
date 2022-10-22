using BusinessLayer.interfaces;
using CommonLayer.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AdminController : ControllerBase
    {
        IConfiguration _configuration;
        IAdminBL _adminBL;
        public AdminController(IConfiguration _configuration, IAdminBL adminBL)
        {
            this._configuration = _configuration;
            this._adminBL = adminBL;
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
                return this.Ok(new { success = true, status = 200, token = token, message = $"login successful {adminLogin.email}" });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
