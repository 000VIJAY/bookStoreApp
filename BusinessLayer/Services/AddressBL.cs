using BusinessLayer.interfaces;
using CommonLayer;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        IAddressRL _addressRL;

        public AddressBL(IAddressRL addressRL)
        {
            _addressRL = addressRL;
        }

        public AddAddressModel AddAddress(AddAddressModel addAddress)
        {
            try
            {
                return this._addressRL.AddAddress(addAddress);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public UpdateAddressModel updateAddress(UpdateAddressModel updateAddress, int AddressId)
        {
            try
            {
                return this._addressRL.updateAddress(updateAddress,AddressId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteAddress(int AddressId)
        {
            try
            {
                return this._addressRL.DeleteAddress(AddressId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
