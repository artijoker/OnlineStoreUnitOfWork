﻿namespace HttpApiServer
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IAccountRepository AccountRepository { get; }
        ICartRepository CartRepository { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
