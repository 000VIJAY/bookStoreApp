using BusinessLayer.interfaces;
using CommonLayer;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {
        IOrderRL _orderRL;
       public OrderBL(IOrderRL orderRL)
        {
            _orderRL = orderRL;
        }

        public AddOrderModel AddOrder(AddOrderModel addOrderModel)
        {
            try
            {
                return this._orderRL.AddOrder(addOrderModel);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteOrder(int Orderid)
        {
            try
            {
                return this._orderRL.DeleteOrder(Orderid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetAllOrderModel> GetAllOrder(int userId)
        {
            try
            {
                return this._orderRL.GetAllOrder(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
