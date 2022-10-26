using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        IConfiguration _iconfiguration;

        public BookRL(IConfiguration iconfiguration)
        {
            this._iconfiguration = iconfiguration;
        }
        public BookModel addBook(BookModel book)
        {
                SqlConnection conn = new SqlConnection(this._iconfiguration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand cmd = new SqlCommand("spAddBook",conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@bookImg", book.Bookimg);
                cmd.Parameters.AddWithValue("@Rating", book.Rating);
                cmd.Parameters.AddWithValue("@RatingCount", book.RatingCount);
                cmd.Parameters.AddWithValue("@bookName", book.bookName);
                cmd.Parameters.AddWithValue("@Description", book.Description);
                cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                cmd.Parameters.AddWithValue("@bookOriginalPrice", book.bookOriginalPrice);
                cmd.Parameters.AddWithValue("@bookDiscountedPrice", book.bookDiscountedPrice);
                cmd.Parameters.AddWithValue("@bookQuantity", book.bookQuantity);
                cmd.ExecuteNonQuery();
                return book;
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
        public bool removeBook(int bookId)
        {
            SqlConnection conn = new SqlConnection(this._iconfiguration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand command = new SqlCommand("spDeleteBook",conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                command.Parameters.AddWithValue("@bookId",bookId);
                var res = command.ExecuteNonQuery();
                if (res > 0)
                {
                 return true;
                }
                else
                {
                    return false;
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

        public UpdateBookModel UpdateBook(UpdateBookModel book, int bookId)
        {
            SqlConnection conn = new SqlConnection(this._iconfiguration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand cmd = new SqlCommand("spUpdateBook", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.Parameters.AddWithValue("@bookImg", book.Bookimg);
                cmd.Parameters.AddWithValue("@Rating", book.Rating);
                cmd.Parameters.AddWithValue("@RatingCount", book.RatingCount);
                cmd.Parameters.AddWithValue("@bookDiscountedPrice", book.bookDiscountedPrice);
                cmd.Parameters.AddWithValue("@bookQuantity", book.bookQuantity);
                var res= cmd.ExecuteNonQuery();
                if (res <= 0)
                {
                    return null;
                }
                return book;
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
        public BookModel getBooksById(int bookId)
        {
            SqlConnection conn = new SqlConnection(this._iconfiguration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand sql = new SqlCommand("select * from dbo.Book where bookId=@bookId",conn);
                conn.Open();
                sql.Parameters.AddWithValue("@bookId", bookId);
                SqlDataReader res =sql.ExecuteReader();
                if (res.HasRows)
                {
                    //return res;
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

        public List<BookModel> getAllBooks()
        {
            SqlConnection conn = new SqlConnection(this._iconfiguration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand sql = new SqlCommand("select * from dbo.Book", conn);
                conn.Open();
                SqlDataReader res = sql.ExecuteReader();
                if (res.HasRows)
                {
                    //return res;
                }
                return null;
            }
            catch (Exception ex)
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
