using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.interfaces
{
    public interface IWishListBL
    {
        AddWishListModel AddWishList(AddWishListModel model);
        bool DeleteFromWishList(int wishListId);
        List<GetWishListModel> GetWishList();

    }
}
