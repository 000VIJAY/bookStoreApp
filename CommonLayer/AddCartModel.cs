using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class AddCartModel
    {
        [Required]
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public int bookId { get; set; }
    }
}
