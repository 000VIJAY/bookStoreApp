using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.interfaces
{
    public interface IFeedbackBL
    {
        AddFeedbackModel Addfeedback(AddFeedbackModel addFeedbackM);
        List<GetFeedbackModel> GetFeedback(int bookId);
    }
}
