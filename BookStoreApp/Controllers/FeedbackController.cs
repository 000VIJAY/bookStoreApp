using BusinessLayer.interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class FeedbackController : ControllerBase
    {
        IFeedbackBL _feedbackBL;

        public FeedbackController(IFeedbackBL feedbackBL)
        {
            _feedbackBL = feedbackBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(AddFeedbackModel model)
        {
            try {
                try
                {
                    var res = this._feedbackBL.Addfeedback(model);
                    if (res == null)
                    {
                        return this.BadRequest(new { success = false, status = 400, message = "Feddback not added" });
                    }
                    return this.Ok(new { success = true, status = 200, message = "feedback Added successfully", value = res });
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpGet("GetFeedbackByBookId/{bookId}")]
        public IActionResult GetFeedbackByBookId(int bookId)
        {
            try
            {
                try
                {
                    var res = this._feedbackBL.GetFeedback(bookId);
                    if (res == null)
                    {
                        return this.BadRequest(new { success = false, status = 400, message = "no feedback" });
                    }
                    return this.Ok(new { success = true, status = 200,value = res });
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
