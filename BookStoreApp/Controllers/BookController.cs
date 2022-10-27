using BusinessLayer.interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BookController : ControllerBase
    {
        IConfiguration _Config;
        IBookBL _BookBL;
        public BookController(IConfiguration config, IBookBL bookBL)
        {
            _Config = config;
            _BookBL = bookBL;
        }
        [Authorize(Roles =Role.Admin)]
        [HttpPost("AddBook")]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            { 
                if (bookModel.bookDiscountedPrice > bookModel.bookOriginalPrice)
                {
                    this.BadRequest(new { success = false, status = 400, message = "Discounted price should be less than or equal to original price" });
                }
                this._BookBL.addBook(bookModel);
               return this.Ok(new { success = true, status = 200, message ="Book Added successfully" });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("RemoveBook/{bookId}")]
        public IActionResult RemoveBook(int bookId)
        {
            try
            {
               var res = this._BookBL.removeBook(bookId);
                if (res == false)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Book not exist" });
                }
                return this.Ok(new { success = true, status = 200, message = "Book Removed successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateBook/{bookId}")]
        public IActionResult UpdateBook(UpdateBookModel bookModel, int bookId)
        {
            try
            {
                var res = this._BookBL.UpdateBook(bookModel,bookId);
                if (res == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Book not exist" });
                }
                return this.Ok(new { success = true, status = 200, message = "Book Updated successfully", value=res});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetBookById/{bookId}")]
        public IActionResult GetBookById(int bookId)
        {
            try
            {
                var res = this._BookBL.getBooksById(bookId);
                if (res == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Book not exist" });
                }
                return this.Ok(new { success = true, status = 200, value = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var res = this._BookBL.getAllBooks();
                if (res == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "no book is available" });
                }
                return this.Ok(new { success = true, status = 200, value = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
