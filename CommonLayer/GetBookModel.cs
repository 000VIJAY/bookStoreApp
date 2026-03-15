using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class GetBookModel
    {
        public int bookId { get; set; }
        public string bookName { get; set; }

        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string Bookimg { get; set; }
        public int Rating { get; set; }
        public int RatingCount { get; set; }
        public decimal bookOriginalPrice { get; set; }
        public decimal bookDiscountedPrice { get; set; }
        public int bookQuantity { get; set; }
    }
}
