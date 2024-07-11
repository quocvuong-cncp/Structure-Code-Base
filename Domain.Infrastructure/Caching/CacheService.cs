﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Application.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Domain.Infrastructure.Caching;
public class CacheService : ICacheService
{
    /**
     * Because we don't have any method to get all of the keys in redis
     * => Solution: Store key in memory at set value to redis
     * 
     * =>> Cache Service can be used concurrently, so we have to make sure that the data structure that we choose is thead safe
     */
    private static readonly ConcurrentDictionary<string, bool> CacheKeys = new ConcurrentDictionary<string, bool>();
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions cacheEntryOptions;

    //public CacheService(IDistributedCache distributedCache)
    //{
    //    _distributedCache = distributedCache;
    //    cacheEntryOptions = new DistributedCacheEntryOptions()
    //    {
    //        SlidingExpiration = TimeSpan.FromSeconds(100)
    //    };
    //}
    public CacheService()
    {
        //_distributedCache = distributedCache;
        cacheEntryOptions = new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromSeconds(100)
        };
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        where T : class
    {
        string? cacheValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (cacheValue is null)
            return null;
        T? value = JsonConvert.DeserializeObject<T>(cacheValue);
        return value;
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
    {
        string cacheValue = JsonConvert.SerializeObject(value);
        await _distributedCache.SetStringAsync(key, cacheValue, cacheEntryOptions, cancellationToken);

        CacheKeys.TryAdd(key, false);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RemoveAsync(key, cancellationToken);
        CacheKeys.TryRemove(key, out bool _);
    }

    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        //foreach (string key in CacheKeys.Keys)
        //{
        //    if (key.StartsWith(prefixKey))
        //        await RemoveAsync(key, cancellationToken); // Call remove one by one
        //}

        IEnumerable<Task> tasks = CacheKeys.Keys.Where(k => k.StartsWith(prefixKey))
            .Select(k => RemoveAsync(k, cancellationToken));

        await Task.WhenAll(tasks); // Execute in parallel
    }


}
