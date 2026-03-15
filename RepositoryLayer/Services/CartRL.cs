using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CartRL :ICartRL
    {
        IConfiguration _configuration;
        public CartRL(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public AddCartModel AddToCart(AddCartModel model)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
               SqlCommand command = new SqlCommand("spAddCart",conn);
                conn.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userId", model.UserId);
                command.Parameters.AddWithValue("@Quantity", model.Quantity);
                command.Parameters.AddWithValue("@bookId", model.bookId);
                var response = command.ExecuteNonQuery();
                if(response >0 )
                {
                    return model;
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
            finally
            {
                conn.Close();
            }
        }
        public UpdateCartModel UpdateCart(int CartId, UpdateCartModel model)
        {
            SqlConnection connection = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand cmd = new SqlCommand("spUpdateCart",connection);
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CartId", CartId);
                cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
               var value =  cmd.ExecuteNonQuery();
                if(value > 0)
                {
                    return model;
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
            finally
            {
                connection.Close();
            }
        }

        public bool DeleteCart(int CartId)
        {
            SqlConnection connection = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand command = new SqlCommand("delete from dbo.cart  where cartId = @bookId", connection);
                connection.Open();
                command.Parameters.AddWithValue("@bookId", CartId);
                var value = command.ExecuteNonQuery();
                if( value > 0 )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<GetCartModel> getAllCart()
        {
            SqlConnection connection = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand command = new SqlCommand("select * from dbo.cart",connection);
                connection.Open();
                var response = command.ExecuteReader();
                List<GetCartModel> mod = new List<GetCartModel>();
                if(response.HasRows)
                {
                    while(response.Read())
                    {
                        GetCartModel model = new GetCartModel();
                        model.userId = Convert.ToInt32(response["userId"]);
                        model.cartId = Convert.ToInt32(response["cartId"]);
                        model.bookId = Convert.ToInt32(response["bookId"]);
                        model.Quantity = Convert.ToInt32(response["Quantity"]);
                        mod.Add(model);
                    }
                    return mod;
                }
                return null;
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
