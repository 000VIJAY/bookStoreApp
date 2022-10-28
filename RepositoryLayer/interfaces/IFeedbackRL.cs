using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
    public interface IFeedbackRL
    {
        AddFeedbackModel Addfeedback(AddFeedbackModel addFeedbackM);
        List<GetFeedbackModel> GetFeedback(int bookId);
    }
}
