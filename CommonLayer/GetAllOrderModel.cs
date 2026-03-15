using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class GetAllOrderModel
    {
        public int userId { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal totalPrice { get; set; }
         public int bookId { get; set; }
        public string bookImg { get; set; }
        public string bookName { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int cartId { get; set; }
        public int Quantity { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
       public int typeId { get; set; }
    }

}
