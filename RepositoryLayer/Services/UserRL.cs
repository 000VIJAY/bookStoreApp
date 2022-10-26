using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Experimental.System.Messaging;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        IConfiguration _configuration;
        public UserRL(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public void RegisterUser(RegisterModel register)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore")))
                {
                    SqlCommand cmd = new SqlCommand("spRegisterUser",conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Name", register.Name);
                    cmd.Parameters.AddWithValue("@email",register.email);
                    cmd.Parameters.AddWithValue("@password", register.password);
                    cmd.Parameters.AddWithValue("@PhoneNumber", register.PhoneNumber);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string LoginUser(loginModel login)
        {   
          using (SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore")))
          {
            try 
            { 
                 SqlCommand cmd = new SqlCommand("spLoginUser", conn);
                 cmd.CommandType = System.Data.CommandType.StoredProcedure;
                 conn.Open();
                 cmd.Parameters.AddWithValue("@email", login.email);
                 cmd.Parameters.AddWithValue("@password", login.password);
                 var val = cmd.ExecuteScalar();
                 if (val != null)
                 {
                        SqlCommand command = new SqlCommand("select userId from dbo.userRegistration where email = @email", conn);
                        command.Parameters.AddWithValue("@email", login.email);
                        var id = command.ExecuteScalar();
                        var newId = Convert.ToInt32(id);
                        return GenerateJwtToken(login.email, newId);
                 }
                 else
                 {
                   return null;
                 }
            }
              catch(Exception ex)
              {
                 throw ex;
              }
          }
           
        }
        private string GenerateJwtToken(string email, int id)
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
                    new Claim("userId",id.ToString()),
                    new Claim(ClaimTypes.Role, "User"),
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ForgotPassword(string email)
        {
          
           using(SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore")))
           {
                try
                {
                    SqlCommand command = new SqlCommand("spForgotPasswordUser",conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email",email);
                    conn.Open();
                    var val = command.ExecuteScalar();
                    if(val == null)
                    {
                        return false;
                    }
                    SqlCommand comm = new SqlCommand("select userId from dbo.userRegistration where email = @email", conn);
                    comm.Parameters.AddWithValue("@email",email);
                    var id = comm.ExecuteScalar();
                    var newId = Convert.ToInt32(id);

                    MessageQueue bookStoreQ = new MessageQueue();
                    //Setting the QueuPath where we want to store the messages.
                    bookStoreQ.Path = @".\private$\funDoNote";
                    if (MessageQueue.Exists(bookStoreQ.Path))
                    {
                        bookStoreQ = new MessageQueue(@".\private$\funDoNote");
                        //Exists
                    }
                    else
                    {
                        // Creates the new queue named "funDoNote"
                        MessageQueue.Create(bookStoreQ.Path);
                    }
                    Message message = new Message();
                    message.Formatter = new BinaryMessageFormatter();
                    message.Body = GenerateJwtToken(email, newId);
                    message.Label = "Forget Password Email";
                    bookStoreQ.Send(message);
                    Message msg = bookStoreQ.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailServices.SendEmail(email, message.Body.ToString());
                    bookStoreQ.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                    bookStoreQ.BeginReceive();
                    bookStoreQ.Close();
                    return true;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
           }
        }
        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailServices.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " + "Queue might be a system queue.");
                }
            }
        }
        private string GenerateToken(string email)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("email", email)
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(ResetPasswordModel resetPasswordModel ,string email)
        { 
            try
            {
                using (SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore")))
                {
                    SqlCommand command = new SqlCommand("spResetPasswordUser",conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    SqlCommand comm = new SqlCommand("select userId from dbo.userRegistration where email = @email", conn);
                    comm.Parameters.AddWithValue("@email", email);
                    var id = comm.ExecuteScalar();
                    var newId = Convert.ToInt32(id);

                    command.Parameters.AddWithValue("@password", resetPasswordModel.Newpassword);
                    command.Parameters.AddWithValue("@userId", newId);
                    var val = command.ExecuteNonQuery();
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
