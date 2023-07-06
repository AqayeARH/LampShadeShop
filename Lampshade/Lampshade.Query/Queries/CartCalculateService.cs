using _0.Framework.Application.Authentication;
using _0.Framework.Infrastructure;
using DiscountManagement.Infra.EfCore;
using Lampshade.Query.Contracts;
using ShopManagement.Application.Contracts.Order;

namespace Lampshade.Query.Queries
{
    public class CartCalculateService : ICartCalculateService
    {
        #region Constractor Injection

        private readonly DiscountContext _discountContext;
        private readonly IAuthenticationHelper _authenticationHelper;
        public CartCalculateService(DiscountContext discountContext, IAuthenticationHelper authenticationHelper)
        {
            _discountContext = discountContext;
            _authenticationHelper = authenticationHelper;
        }

        #endregion

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();

            var currentAccountRole = _authenticationHelper.CurrentAccountRole();

            var colleagueDiscounts = _discountContext.ColleaguesDiscounts
                .Where(x => x.IsRemoved == false)
                .ToList();

            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .ToList();

            foreach (var cartItem in cartItems)
            {
                if (_authenticationHelper.IsAuthenticated())
                {
                    if (currentAccountRole == Roles.User)
                    {
                        var customerDiscount = customerDiscounts
                            .FirstOrDefault(x => x.ProductId == cartItem.Id);

                        if (customerDiscount != null)
                            cartItem.DiscountRate = customerDiscount.DiscountRate;
                    }
                    else
                    {
                        var colleagueDiscount = colleagueDiscounts
                            .FirstOrDefault(x => x.ProductId == cartItem.Id);

                        if (colleagueDiscount != null)
                            cartItem.DiscountRate = colleagueDiscount.DiscountRate;
                    }
                }

                cartItem.DiscountAmount = (cartItem.DiscountRate * cartItem.TotalItemPrice) / 100;
                cartItem.ItemPayAmount = cartItem.TotalItemPrice - cartItem.DiscountAmount;
                cart.AddItem(cartItem);
            }

            /*
            if (currentAccountRole != Roles.User)
            {
                foreach (var cartItem in cartItems)
                {
                    var customerDiscount = customerDiscounts
                        .FirstOrDefault(x => x.ProductId == cartItem.Id);

                    if (customerDiscount == null)
                        continue;

                    cartItem.DiscountRate = customerDiscount.DiscountRate;
                    cartItem.DiscountAmount = (cartItem.DiscountRate * cartItem.TotalItemPrice) / 100;
                    cartItem.ItemPayAmount = cartItem.TotalItemPrice - cartItem.DiscountAmount;
                    cart.AddItem(cartItem);
                }
            }
            else
            {
                foreach (var cartItem in cartItems)
                {
                    var colleagueDiscount = colleagueDiscounts
                        .FirstOrDefault(x => x.ProductId == cartItem.Id);

                    if (colleagueDiscount == null)
                        continue;

                    cartItem.DiscountRate = colleagueDiscount.DiscountRate;
                    cartItem.DiscountAmount = (cartItem.DiscountRate * cartItem.TotalItemPrice) / 100;
                    cartItem.ItemPayAmount = cartItem.TotalItemPrice - cartItem.DiscountAmount;
                    cart.AddItem(cartItem);
                }
            }
            */

            return cart;
        }
    }
}
