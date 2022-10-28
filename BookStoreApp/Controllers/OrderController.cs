using BusinessLayer.interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class OrderController : ControllerBase
    {
        IOrderBL _orderBL;

        public OrderController(IOrderBL orderBL)
        {
            _orderBL = orderBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AddOrder")]
        public IActionResult AddOrder(AddOrderModel model)
        {
            try
            {
                var res = this._orderBL.AddOrder(model);
                if(res == null)
                {
                   return this.BadRequest(new {success=false , status = 400,message="Order is not placed"});
                }
                return this.Ok(new { success = true, status = 200, message = "Order placed successfully", value = res });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteOrder/{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            try
            {
                var res = this._orderBL.DeleteOrder(orderId);
                if (res == false)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Order is not deleted" });
                }
                return this.Ok(new { success = true, status = 200, message = "Order Deleted successfully", value = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllOrder/{userId}")]
        public IActionResult GetAllOrder(int userId)
        {
            try
            {
                var res = this._orderBL.GetAllOrder(userId);
                if (res == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "no order exist" });
                }
                return this.Ok(new { success = true, status = 200, value = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

