using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishListRL : IWishListRL
    {
        readonly IConfiguration _configuration;
        public WishListRL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AddWishListModel AddWishList(AddWishListModel model)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand cmd = new SqlCommand("spAddToWishList",conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@userId", model.userId);
                cmd.Parameters.AddWithValue("@bookId", model.bookId);
                var res = cmd.ExecuteNonQuery();
                if(res > 0)
                {
                    return model;
                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        public bool DeleteFromWishList(int wishListId)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand command = new SqlCommand("delete from dbo.WishList  where wishListId = @wishListId", conn);
                conn.Open();
                command.Parameters.AddWithValue("@wishListId", wishListId);
                var res = command.ExecuteNonQuery();
                if(res>0)
                {
                    return true;
                }
                return false;
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

        public List<GetWishListModel> GetWishList()
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
                List<GetWishListModel> list = new List<GetWishListModel>();
                SqlCommand command = new SqlCommand("select * from dbo.WishList", conn);
                conn.Open();
                var res = command.ExecuteReader();
                if(res.HasRows)
                {
                    while(res.Read())
                    {
                        GetWishListModel getWish = new GetWishListModel();
                        getWish.wishListId = Convert.ToInt32(res["wishListId"]);
                        getWish.bookId = Convert.ToInt32(res["bookId"]);
                        getWish.userId = Convert.ToInt32(res["userId"]);
                        list.Add(getWish);
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
