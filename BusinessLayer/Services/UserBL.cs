using BusinessLayer.interfaces;
using CommonLayer;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        readonly IUserRL _userRL;
        public UserBL(IUserRL userRL)
        {
            this._userRL = userRL;
        }
        public void RegisterUser(RegisterModel register)
        {
            try
            {
                this._userRL.RegisterUser(register);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string LoginUser(loginModel login)
        {
            try
            {
                return this._userRL.LoginUser(login);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ForgotPassword(string email)
        {
            try
            {
                return this._userRL.ForgotPassword(email);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
