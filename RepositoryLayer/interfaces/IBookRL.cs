using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
    public interface IBookRL
    {
        BookModel addBook(BookModel book);
        bool removeBook(int bookId);
        UpdateBookModel UpdateBook(UpdateBookModel book,int bookId);
        BookModel getBooksById(int bookId);
        List<BookModel> getAllBooks();
    }
}
