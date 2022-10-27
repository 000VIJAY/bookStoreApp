using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.interfaces
{
    public interface IAddressBL
    {
        AddAddressModel AddAddress(AddAddressModel addAddress);
        UpdateAddressModel updateAddress(UpdateAddressModel updateAddress, int AddressId);
        bool DeleteAddress(int AddressId);
    }
}
