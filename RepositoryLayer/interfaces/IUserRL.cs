using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
    public interface IUserRL
    {
        void RegisterUser(RegisterModel register);
        string LoginUser(loginModel login);
        bool ForgotPassword(string email);
    }
}
