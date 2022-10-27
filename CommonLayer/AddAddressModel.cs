using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class AddAddressModel
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int userId { get; set; }
        [Required]
        [RegularExpression("^[1-3]{1}", ErrorMessage = "Enter number between 1 to 3 only")]
        public int typeId { get; set; }
    }
}
