using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class UpdateBookModel
    {
        [RegularExpression("^[1-5]{1}$", ErrorMessage = "Rating will be 1 to 5 only")]

        public int Rating { get; set; }
        public string Bookimg { get; set; }
        public int RatingCount { get; set; }
        public int bookDiscountedPrice { get; set; }
        public int bookQuantity { get; set; }
    }
}
