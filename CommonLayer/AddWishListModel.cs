using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class AddWishListModel
    {
        [Required]
        public int userId { get; set; }
        [Required]
        public int bookId { get; set; }
    }
}
