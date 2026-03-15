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
    public class WishListController : ControllerBase
    {
        IConfiguration _configuration;
        IWishListBL _WishListBL;
        public WishListController(IConfiguration _configuration,IWishListBL WishListBL)
        {
            this._configuration = _configuration;
            this._WishListBL = WishListBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AddToWishList")]
        public IActionResult AddToWishList(AddWishListModel model)
        {
            try
            {
                var res = this._WishListBL.AddWishList(model);
                if(res==null)
                {
                    return this.BadRequest(new { success = false, response = 401, message = "not added to wishList" });
                }
                return this.Ok(new {success=true,Response=200, message="Book added to wish list successfully", value = res });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteFromWishList/{wishListId}")]
        public IActionResult DeleteFromWishList(int wishListId)
        {
            try
            {
                var res = this._WishListBL.DeleteFromWishList(wishListId);
                if (res == false)
                {
                    return this.BadRequest(new { success = false, response = 401, message = "wishList does not exist" });
                }
                return this.Ok(new { success = true, Response = 200, message = " wish list deleted successfully", value = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetWishList")]
        public IActionResult GetWishList()
        {
            try
            {
                var res = this._WishListBL.GetWishList();
                if (res == null)
                {
                    return this.BadRequest(new { success = false, response = 401, message = "wish list is empty" });
                }
                return this.Ok(new { success = true, Response = 200, value = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
