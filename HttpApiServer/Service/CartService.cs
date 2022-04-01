using HttpModels;

namespace HttpApiServer
{
    public class CartService
    {
        private readonly IUnitOfWork _unit;

        public CartService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task AddProduct(Guid userId, Guid productId, int quantity = 1)
        {
            var cart = await _unit.CartRepository.GetCartByAccountId(userId);
            var cartItem = cart.CartItems.SingleOrDefault(i => i.ProductId == productId);

            if (cartItem is not null)
            {
                cartItem.Quantity += quantity;
                //await _unit.CartRepository.Update(cart);
            }
            else
            {
                cartItem = new CartItem() { Id = Guid.NewGuid(), CartId = cart.Id, ProductId = productId, Quantity = quantity };
                await _unit.CartRepository.AddItem(cartItem);
                cart.CartItems.Add(cartItem);
            }
            await _unit.CartRepository.Update(cart);
            await _unit.SaveChangesAsync();

        }

        public async Task<Cart> GetUserCart(Guid accountId)
        {
            return await _unit.CartRepository.GetCartByAccountId(accountId);
        }

    }
}
