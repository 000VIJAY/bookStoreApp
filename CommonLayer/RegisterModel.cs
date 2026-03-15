using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z0-9._]+@[A-Za-z0-9]+.[a-z]{2,5}$", ErrorMessage = "Please enter proper email 'ex-Vijay78@gmail.com'")]
        public string email { get; set; }
        [Required]
        [RegularExpression("(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()_+=-])[a-zA-Z0-9!@#$%^&*()_+=-]{8,}$", ErrorMessage = "Password is not valid 1.Min 8 Character , 2.Atleast 1 special character[@,#,$],3.Atleast 1 digit[0-9],4.Atleast 1 Capital Letter[A-Z] ")]
        public string password { get; set; }
        [Required]
        [RegularExpression("[6-9]{1}[0-9]{9}$")]
        public double PhoneNumber { get; set; }
    }
}
