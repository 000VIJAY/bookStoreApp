using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.interfaces
{
    public interface IUserBL
    {
        void RegisterUser(RegisterModel register);
        string LoginUser(loginModel login);
        bool ForgotPassword(string email);
    }
}
