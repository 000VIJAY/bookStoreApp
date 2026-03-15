using BusinessLayer.interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CartController : ControllerBase
    {
        ICartBL _cartBL;
        IConfiguration _configuration; 
        public CartController(ICartBL cartBL,IConfiguration configuration)
        {
            this._cartBL = cartBL;
            this._configuration = configuration;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AddToCart")]
        public IActionResult AddToCart(AddCartModel model)
        {
            try
            {
              var res  = this._cartBL.AddToCart(model);
                if(res == null)
                {
                    return this.BadRequest(new { success = false, status = 401, message = " details is Not added in cart"});
                }
                return this.Ok(new { success = true, status = 200, value = res });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("UpdateCart/{cartId}")]
        public IActionResult UpdateCart(int cartId,UpdateCartModel model)
        {
            try
            {
                var res = this._cartBL.UpdateCart(cartId,model);
                if (res == null)
                {
                    return this.BadRequest(new { success = false, status = 401, message = "Cart not updated" });
                }
                return this.Ok(new { success = true, status = 200,message="Cart updated successfully", value = res });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteCart/{cartId}")]
        public IActionResult DeleteCart(int cartId)
        {
            try
            {
                var res = this._cartBL.DeleteCart(cartId);
                if (res == false)
                {
                    return this.BadRequest(new { success = false, status = 401, message = "Cart not exist" });
                }
                return this.Ok(new { success = true, status = 200, message = "Cart deleted successfully", value = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllCart")]
        public IActionResult GetAllCart()
        {
            try
            {
                var res = this._cartBL.getAllCart();
                if (res == null)
                {
                    return this.BadRequest(new { success = false, status = 401, message = "Cart not exist" });
                }
                return this.Ok(new { success = true, status = 200,value = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
