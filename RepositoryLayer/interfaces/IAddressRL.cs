using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
    public interface IAddressRL
    {
        AddAddressModel AddAddress(AddAddressModel addAddress);
        UpdateAddressModel updateAddress(UpdateAddressModel updateAddress, int AddressId);
        bool DeleteAddress(int AddressId);
    }
}
