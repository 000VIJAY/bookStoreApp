using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.interfaces
{
    public interface IBookBL
    {
        BookModel addBook(BookModel book);
        bool removeBook(int bookId);

        UpdateBookModel UpdateBook(UpdateBookModel book, int bookId);

        GetBookModel getBooksById(int bookId);
        List<GetBookModel> getAllBooks();
    }
}
