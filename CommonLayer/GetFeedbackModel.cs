using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class GetFeedbackModel
    {
        public int FeedbackId { get; set; }
        public int Rating { get; set; }
        public string comment { get; set; }

        public int bookId { get; set; }
        public int userId { get; set; }
    }
}
