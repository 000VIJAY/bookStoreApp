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
                cmd.Parameters.AddWithValue("@bookimg", book.Bookimg);
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
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.Parameters.AddWithValue("@bookimg", book.Bookimg);
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
        public GetBookModel getBooksById(int bookId)
        {
            SqlConnection conn = new SqlConnection(this._iconfiguration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand sql = new SqlCommand("select * from dbo.Book where bookId=@bookId",conn);
                conn.Open();
                sql.Parameters.AddWithValue("@bookId", bookId);
                GetBookModel books = new GetBookModel();
                SqlDataReader res =sql.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        books.bookId = res.GetInt32(0);
                        books.Bookimg = res.GetString(1);
                        books.Rating = res.GetInt32(2);
                        books.RatingCount = res.GetInt32(3);
                        books.bookName = res.GetString(4);
                        books.Description = res.GetString(5);
                        books.AuthorName = res.GetString(6);
                        books.bookOriginalPrice = res.GetDecimal(7);
                        books.bookDiscountedPrice = res.GetDecimal(8);
                        books.bookQuantity = res.GetInt32(9);
                    }
                    return books;
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

        public List<GetBookModel> getAllBooks()
        {
            SqlConnection conn = new SqlConnection(this._iconfiguration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand sql = new SqlCommand("select * from dbo.Book", conn);
                conn.Open();
                SqlDataReader res = sql.ExecuteReader();
                List < GetBookModel > books = new List<GetBookModel>();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        GetBookModel bookModel = new GetBookModel();
                        bookModel.bookId = Convert.ToInt32(res["BookId"]);
                        bookModel.bookName = res["bookName"].ToString();
                        bookModel.Description = res["Description"].ToString();
                        bookModel.AuthorName = res["AuthorName"].ToString();
                        bookModel.Bookimg = res["bookimg"].ToString();
                        bookModel.Rating = Convert.ToInt32(res["Rating"]);
                        bookModel.RatingCount = Convert.ToInt32(res["RatingCount"]);
                        bookModel.bookOriginalPrice = Convert.ToInt32(res["bookOriginalPrice"]);
                        bookModel.bookDiscountedPrice = Convert.ToInt32(res["bookDiscountedPrice"]);
                        bookModel.bookQuantity = Convert.ToInt32(res["bookQuantity"]);
                        books.Add(bookModel);
                    }
                    return books;
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
