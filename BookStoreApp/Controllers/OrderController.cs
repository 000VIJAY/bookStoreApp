using BusinessLayer.interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class OrderController : ControllerBase
    {
        IOrderBL _orderBL;
        private readonly IDistributedCache _cache;
        private readonly IMemoryCache _memoryCache;

        public OrderController(IOrderBL orderBL, IDistributedCache cache, IMemoryCache memoryCache)
        {
            _orderBL = orderBL;
            this._cache = cache;
            this._memoryCache = memoryCache;
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
        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllOrderUsingCache/{userId}")]
        public IActionResult GetAllOrderUsingCache(int userId)
        {
            try
            {
                string CacheKey = "UserList";
                string serializeNoteList;
                var res = new List<GetAllOrderModel>();
                var redisNoteList = _cache.Get(CacheKey);
                if (redisNoteList != null)
                {
                    serializeNoteList = Encoding.UTF8.GetString(redisNoteList);
                    res = JsonConvert.DeserializeObject<List<GetAllOrderModel>>(serializeNoteList);
                }
                else
                {
                    res = this._orderBL.GetAllOrder(userId);
                    serializeNoteList = JsonConvert.SerializeObject(res);
                    redisNoteList = Encoding.UTF8.GetBytes(serializeNoteList);
                    var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)).SetAbsoluteExpiration(TimeSpan.FromHours(6));
                    _cache.Set(CacheKey, redisNoteList, option);

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

