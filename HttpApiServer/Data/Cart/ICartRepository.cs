using HttpModels;

namespace HttpApiServer
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task AddItem(CartItem item);
        Task<Cart> GetCartByAccountId(Guid id);
        Task<Cart?> FindCartByAccountId(Guid id);
    }
}
