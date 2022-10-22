using CommonLayer.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
    public interface IAdminRL
    {
        string LoginAdmin(AdminLoginModel adminLogin);
    }
}
