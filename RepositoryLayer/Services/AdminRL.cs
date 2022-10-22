using CommonLayer.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        IConfiguration _configuration;
        public AdminRL(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string LoginAdmin(AdminLoginModel adminLogin)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore")))
                { 
                    SqlCommand cmd = new SqlCommand("spLoginAdmin",conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", adminLogin.email);
                    cmd.Parameters.AddWithValue("@password",adminLogin.password);
                    conn.Open();
                    var val = cmd.ExecuteScalar();
                    if (val != null)
                    {
                        SqlCommand command = new SqlCommand("select userId from dbo.userRegistration where email = @email", conn);
                        command.Parameters.AddWithValue("@email", adminLogin.email);
                        var id = command.ExecuteScalar();
                        var newId = Convert.ToInt32(id);
                        return GenerateJwtToken(adminLogin.email, newId);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string GenerateJwtToken(string email,int AdminId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("email", email),
                    new Claim("AdminId",AdminId.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials =
                    new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
