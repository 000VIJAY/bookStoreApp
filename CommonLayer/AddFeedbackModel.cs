using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class AddFeedbackModel
    {
        [RegularExpression("^[1-5]{1}$",ErrorMessage="Rating will be between 1 to 5 only")]
        public int Rating { get; set; }
        public string comment { get; set; }

        public int bookId { get; set; }
        public int userId { get; set; }
    }
}
