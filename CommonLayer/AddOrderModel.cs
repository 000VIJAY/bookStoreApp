using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class AddOrderModel
    {
        public int AddressId { get; set; }
        public int cartId { get; set; }
        public  int bookId { get; set; }
        public int userId { get; set; }
    }
}
