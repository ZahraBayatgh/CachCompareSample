using LazyCache;
using Microsoft.Extensions.Caching.Memory;

//MemoryCacheMethod();

void MemoryCacheMethod()
{
    var cache = new MemoryCache(new MemoryCacheOptions());
    var counter = 0;
    object o=new object();

    Parallel.ForEach(Enumerable.Range(1, 20), _ =>
    {
        var cachedItem = cache.GetOrCreate("key", _ =>
        {
            lock (o)
            {
                Thread.Sleep(1000);
                return counter++;
            } 
        });
        Console.Write($"{cachedItem} ");
    });
}

LazyCacheMethod();
void LazyCacheMethod()
{
    IAppCache cache = new CachingService();
    var counter = 0;
    object o = new object();
    Parallel.ForEach(Enumerable.Range(1, 20), _ =>
    {
        var cachedItem = cache.GetOrAdd("key", _ =>
        {
            lock (o)
            {
                Thread.Sleep(1000); return counter++;
            }
        });

        Console.Write($"{cachedItem} ");
    });
}