using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
    public interface ICartRL
    {
        AddCartModel AddToCart(AddCartModel model);
        UpdateCartModel UpdateCart(int CartId,UpdateCartModel model);

        bool DeleteCart(int CartId);
        List<GetCartModel> getAllCart();
    }
}
