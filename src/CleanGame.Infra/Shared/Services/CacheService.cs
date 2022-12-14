using CleanGame.Application.Common.Interfaces;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace CleanGame.Infra.Shared.Services;

public class CacheService : ICache
{
    private readonly IRedisClient _redis;

    public CacheService(IRedisClient redis)
    {
        _redis = redis;
    }

    public Task AddOrUpdateAsync<T>(string key, T item, TimeSpan expireTime)
        => _redis.GetDefaultDatabase().AddAsync(key, item, expireTime);

    public Task AddOrUpdateAllAsync<T>(IDictionary<string, T> data, TimeSpan expireTime)
    {
        var redisData = data.Select(_ => new Tuple<string, T>(_.Key, _.Value));
        return _redis.GetDefaultDatabase().AddAllAsync(redisData.ToArray(), expireTime);
    }

    public Task AddSetAsync<T>(string key, string itemKey, T data)
        => _redis.GetDefaultDatabase().HashSetAsync(key, itemKey, data);

    public Task AddAllSetAsync<T>(string key, IDictionary<string, T> data)
        => _redis.GetDefaultDatabase().HashSetAsync(key, data);

    public Task<T?> GetAsync<T>(string key)
        => _redis.GetDefaultDatabase().GetAsync<T>(key);

    public Task<IDictionary<string, T?>> GetAsync<T>(IEnumerable<string> keys)
        => _redis.GetDefaultDatabase().GetAllAsync<T>(keys.ToArray());

    public Task<IEnumerable<string>> GetAllSetKeysAsync<T>(string key)
        => _redis.GetDefaultDatabase().HashKeysAsync(key);

    public Task<T?> GetSetAsync<T>(string key, string itemKey)
        => _redis.GetDefaultDatabase().HashGetAsync<T>(key, itemKey);

    public Task<Dictionary<string, T>> GetAllSetAsync<T>(string key)
        => _redis.GetDefaultDatabase().HashGetAllAsync<T>(key);

    public Task<IEnumerable<T>> GetAllSetValuesAsync<T>(string key)
        => _redis.GetDefaultDatabase().HashValuesAsync<T>(key);

    public Task<Dictionary<string, T?>> GetAllSetByKeyAsync<T>(string key, IList<string> itemKeys)
        => _redis.GetDefaultDatabase().HashGetAsync<T>(key, itemKeys.ToArray());

    public Task RemoveAsync<T>(string key)
        => _redis.GetDefaultDatabase().RemoveAsync(key);

    public async Task RemoveAllAsync()
    {
        var keys = await _redis.GetDefaultDatabase().SearchKeysAsync("*");
        await _redis.GetDefaultDatabase().RemoveAllAsync(keys.ToArray());
    }

    public Task RemoveSetAsync<T>(string key, string itemKey)
        => _redis.GetDefaultDatabase().HashDeleteAsync(key, itemKey);

    public Task RemoveAllSetAsync<T>(string key, IEnumerable<string> itemKeys)
        => _redis.GetDefaultDatabase().HashDeleteAsync(key, itemKeys.ToArray());

    public Task<bool> ExistsAsync<T>(string key)
        => _redis.GetDefaultDatabase().ExistsAsync(key);

    public Task<bool> ExistSetAsync<T>(string key, string itemKey)
        => _redis.GetDefaultDatabase().HashExistsAsync(key, itemKey);

    public ICacheTransaction CreateTransaction()
    {
        var transaction = _redis.GetDefaultDatabase().Database.CreateTransaction();
        return new CacheTransactionService(this, transaction);
    }
}