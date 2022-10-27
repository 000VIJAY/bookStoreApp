using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
    public interface IWishListRL
    {
        AddWishListModel AddWishList(AddWishListModel model);
        bool DeleteFromWishList(int wishListId);
        List<GetWishListModel> GetWishList();
    }
}
