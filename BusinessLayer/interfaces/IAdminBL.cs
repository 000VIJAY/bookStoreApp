using CommonLayer.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.interfaces
{
    public interface IAdminBL
    {
        string LoginAdmin(AdminLoginModel adminLogin);
    }
}
