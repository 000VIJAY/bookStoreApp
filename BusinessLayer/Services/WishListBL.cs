using BusinessLayer.interfaces;
using CommonLayer;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        IWishListRL _wishListRL;
        public WishListBL(IWishListRL wishListRL)
        {
            _wishListRL = wishListRL;
        }

        public AddWishListModel AddWishList(AddWishListModel model)
        {
            try
            {
                return this._wishListRL.AddWishList(model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteFromWishList(int wishListId)
        {
            try
            {
                return this._wishListRL.DeleteFromWishList(wishListId);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<GetWishListModel> GetWishList()
        {
            try
            {
                return this._wishListRL.GetWishList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
