namespace Simple.MemoryCache.Entities
{
    public enum CacheItemPriority
    {
        Default = 0,
        Low = 1,
        BelowNormal = 2,
        Normal = 3,
        AboveNormal = 4,
        High = 5,
        NotRemovable = 6
    }
}