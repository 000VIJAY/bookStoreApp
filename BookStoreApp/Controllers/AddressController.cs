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
    public class AddressController : ControllerBase
    {
        IAddressBL _addressBL;
        IConfiguration _Config;
        public AddressController(IAddressBL addressBL,IConfiguration Config)
        {
            this._addressBL =addressBL;
            this._Config = Config;
        }
        [Authorize(Roles =Role.User)]
        [HttpPost("AddAddress")]
        public IActionResult AddAddress(AddAddressModel model)
        {
            try
            {
                var res = this._addressBL.AddAddress(model);
                if (res == null)
                {
                    return this.BadRequest(new { success = false, status = 401, message = "Cart not exist" });
                }
                return this.Ok(new { success = true, status = 200, message = "Address Added successfully",value = res });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpPut("UpdateAddress")]
        public IActionResult UpdateAddress(UpdateAddressModel model, int AddressId)
        {
            try
            {
                var res = this._addressBL.updateAddress(model,AddressId);
                if (res == null)
                {
                    return this.BadRequest(new { success = false, status = 401, message = "Address Not found" });
                }
                return this.Ok(new { success = true, status = 200, message = "Address updated successfully", value = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteAddress/{AddressId}")]
        public IActionResult DeleteAddress(int AddressId)
        {
            try
            {
                var res = this._addressBL.DeleteAddress(AddressId);
                if (res == false)
                {
                    return this.BadRequest(new { success = false, status = 401, message = "Address Not found" });
                }
                return this.Ok(new { success = true, status = 200, message = "Address deleted successfully", value = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
