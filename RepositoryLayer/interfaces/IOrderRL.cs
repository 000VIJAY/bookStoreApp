using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
    public interface IOrderRL
    {
        AddOrderModel AddOrder(AddOrderModel addOrderModel);
        bool DeleteOrder(int Orderid);
        List<GetAllOrderModel> GetAllOrder(int userId);
    }
}
