using BusinessLayer.interfaces;
using CommonLayer;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        IBookRL _bookRL;
        public BookBL(IBookRL bookRL)
        {
            this._bookRL = bookRL;
        }
        public BookModel addBook(BookModel book)
        {
            try
            {
                return this._bookRL.addBook(book);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool removeBook(int bookId)
        {
            try
            {
                return this._bookRL.removeBook(bookId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public UpdateBookModel UpdateBook(UpdateBookModel book, int bookId)
        {
            try
            {
                return this.UpdateBook(book, bookId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public BookModel getBooksById(int bookId)
        {
            try
            {
               return this._bookRL.getBooksById(bookId);
            }
            catch( Exception ex)
            {
                throw ex;
            }
        }

        public List<BookModel> getAllBooks()
        {
            try
            {
                return this.getAllBooks();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
