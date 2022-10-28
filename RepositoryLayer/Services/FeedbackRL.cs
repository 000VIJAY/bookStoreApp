using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace RepositoryLayer.Services
{
    public class FeedbackRL:IFeedbackRL
    {
        IConfiguration _configuration;
        public FeedbackRL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AddFeedbackModel Addfeedback(AddFeedbackModel addFeedbackM)
        {
                SqlConnection conne = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand cmd = new SqlCommand("spAddFeedback", conne);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conne.Open();
                cmd.Parameters.AddWithValue("@Rating",addFeedbackM.Rating);
                cmd.Parameters.AddWithValue("@comment",addFeedbackM.comment);
                cmd.Parameters.AddWithValue("@bookId",addFeedbackM.bookId);
                cmd.Parameters.AddWithValue("@userId",addFeedbackM.userId);
                var res = cmd.ExecuteNonQuery();
                if(res>0)
                {
                    return addFeedbackM;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conne.Close();
            }
        }

        public List<GetFeedbackModel> GetFeedback(int bookId)
        {
            SqlConnection conne = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand command = new SqlCommand("spGetFeedback", conne);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                conne.Open();
                List<GetFeedbackModel> feedbackBLs = new List<GetFeedbackModel>();
                command.Parameters.AddWithValue("@bookId", bookId);
                var res = command.ExecuteReader();
                if(res.HasRows)
                {
                    while(res.Read())
                    {
                        GetFeedbackModel get = new GetFeedbackModel();
                        get.userId = Convert.ToInt32(res["userId"]);
                        get.bookId = Convert.ToInt32(res["bookId"]);
                        get.FeedbackId = Convert.ToInt16(res["feedbackId"]);
                        get.Rating = Convert.ToInt32(res["Rating"]);
                        get.comment = (res["comment"]).ToString();
                        feedbackBLs.Add(get);
                    }
                    return feedbackBLs;
                }
                return null;
            }catch(Exception ex)
            {
                throw ex;
            }
            finally {
                conne.Close();
            }
        }
    }
}
