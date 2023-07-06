using ShopManagement.Application.Contracts.Order;

namespace Lampshade.Query.Contracts
{
    public interface ICartCalculateService
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }
}
