using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrderRL : IOrderRL
    {
        IConfiguration _configuration;
        public OrderRL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AddOrderModel AddOrder(AddOrderModel addOrderModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand cmd = new SqlCommand("spTotalPriceCart", conn);
                SqlCommand command = new SqlCommand("spAddOrder",conn);
                
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandType = System.Data.CommandType.StoredProcedure;
               conn.Open();
                cmd.Parameters.AddWithValue("@cartId", addOrderModel.cartId);
                var res = (decimal)cmd.ExecuteScalar();
                command.Parameters.AddWithValue("@orderDate",DateTime.Now);
                command.Parameters.AddWithValue("@totalPrice",res);
                command.Parameters.AddWithValue("@AddressId",addOrderModel.AddressId);
                command.Parameters.AddWithValue("@cartId",addOrderModel.cartId);
                command.Parameters.AddWithValue("@bookId",addOrderModel.bookId);
                command.Parameters.AddWithValue("@userId",addOrderModel.userId);
                var response = command.ExecuteNonQuery();
                if(response > 0)
                {
                    return addOrderModel;
                }
                return null;

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool DeleteOrder(int Orderid)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand cmd = new SqlCommand("delete from dbo.[Order] where orderId = @orderId", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@orderId", Orderid);
                var res = cmd.ExecuteNonQuery();
                if(res > 0)
                {
                    return true;
                }
                return false;

            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<GetAllOrderModel> GetAllOrder(int userId)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
                List<GetAllOrderModel> list = new List<GetAllOrderModel>();
                SqlCommand command = new SqlCommand("spGetOrder", conn);
                conn.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userId", userId);
                var res = command.ExecuteReader();
                if(res.HasRows)
                {
                    while(res.Read())
                    {
                        GetAllOrderModel model = new GetAllOrderModel();
                        model.userId = Convert.ToInt32(res["userId"]);
                        model.OrderId = Convert.ToInt32(res["orderId"]);
                        model.OrderDate = Convert.ToDateTime(res["orderDate"]);
                        model.totalPrice = Convert.ToDecimal(res["totalPrice"]);
                        model.bookId = Convert.ToInt32(res["bookId"]);
                        model.bookImg = (res["bookImg"]).ToString();
                        model.bookName = (res["bookName"]).ToString();
                        model.DiscountedPrice = Convert.ToDecimal(res["bookDiscountedPrice"]);
                        model.cartId = Convert.ToInt32(res["cartId"]);
                        model.Quantity = Convert.ToInt32(res["Quantity"]);
                        model.AddressId = Convert.ToInt32(res["AddressId"]);
                        model.Address = (res["Address"]).ToString();
                        model.City = res["City"].ToString();
                        model.State = res["State"].ToString();
                        model.typeId = Convert.ToInt32(res["typeId"]);
                        list.Add(model);
                    }
                    return list;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
