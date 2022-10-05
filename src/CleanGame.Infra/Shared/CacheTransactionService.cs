using CleanGame.Domain.Shared.Interfaces;
using StackExchange.Redis;

namespace CleanGame.Infra.Shared;

public class CacheTransactionService : ICacheTransaction
{
    private readonly ITransaction _transaction;

    public CacheTransactionService(ICache redisCache, ITransaction transaction)
    {
        _transaction = transaction;
        Cache = redisCache;
    }

    public ICache Cache { get; }

    public Task<bool> ExecuteAsync() => _transaction.ExecuteAsync();
}