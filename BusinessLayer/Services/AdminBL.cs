using BusinessLayer.interfaces;
using CommonLayer.Admin;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        IAdminRL _AdminRL;
        public AdminBL(IAdminRL adminRL)
        {
            _AdminRL = adminRL;
        }
        public string LoginAdmin(AdminLoginModel adminLogin)
        {
            try
            {
                return this._AdminRL.LoginAdmin(adminLogin);
            }
            catch(Exception ex )
            {
                throw ex;
            }
        }
    }
}
