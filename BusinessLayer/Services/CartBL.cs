using BusinessLayer.interfaces;
using CommonLayer;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        ICartRL _cartRL;
        public CartBL(ICartRL cartRL)
        {
            _cartRL = cartRL;
        }

        public AddCartModel AddToCart(AddCartModel model)
        {
            try
            {
                return this._cartRL.AddToCart(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UpdateCartModel UpdateCart(int CartId, UpdateCartModel model)
        {
            try
            {
                return this._cartRL.UpdateCart(CartId, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCart(int CartId)
        {
            try
            {
                return this._cartRL.DeleteCart(CartId);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<GetCartModel> getAllCart()
        {
            try
            {
                return this._cartRL.getAllCart();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
