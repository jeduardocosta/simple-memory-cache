using FluentAssertions;
using NUnit.Framework;
using Simple.MemoryCache.Converters;
using Simple.MemoryCache.Entities;

namespace Simple.MemoryCache.UnitTest.Entities.Converters
{
    [TestFixture]
    public class CacheItemPriorityConverterTest
    {
        private ICacheItemPriorityConverter _cacheItemPriorityConverter;

        [SetUp]
        public void SetUp()
        {
            _cacheItemPriorityConverter = new CacheItemPriorityConverter();
        }

        [Test]
        public void Should_ConvertCacheItemPriority_ToDefaultValue()
        {
            const CacheItemPriority cacheItemPriority = CacheItemPriority.Default;

            var obtained = _cacheItemPriorityConverter.Convert(cacheItemPriority);

            obtained.Should().Be(System.Web.Caching.CacheItemPriority.Default);
        }

        [Test]
        public void Should_ConvertCacheItemPriority_WithLowValue()
        {
            const CacheItemPriority cacheItemPriority = CacheItemPriority.Low;

            var obtained = _cacheItemPriorityConverter.Convert(cacheItemPriority);

            obtained.Should().Be(System.Web.Caching.CacheItemPriority.Low);
        }

        [Test]
        public void Should_ConvertCacheItemPriority_WithBelowNormalValue()
        {
            const CacheItemPriority cacheItemPriority = CacheItemPriority.BelowNormal;

            var obtained = _cacheItemPriorityConverter.Convert(cacheItemPriority);

            obtained.Should().Be(System.Web.Caching.CacheItemPriority.BelowNormal);
        }

        [Test]
        public void Should_ConvertCacheItemPriority_WithNormalValue()
        {
            const CacheItemPriority cacheItemPriority = CacheItemPriority.Normal;

            var obtained = _cacheItemPriorityConverter.Convert(cacheItemPriority);

            obtained.Should().Be(System.Web.Caching.CacheItemPriority.Normal);
        }

        [Test]
        public void Should_ConvertCacheItemPriority_WithAboveNormalValue()
        {
            const CacheItemPriority cacheItemPriority = CacheItemPriority.AboveNormal;

            var obtained = _cacheItemPriorityConverter.Convert(cacheItemPriority);

            obtained.Should().Be(System.Web.Caching.CacheItemPriority.AboveNormal);
        }

        [Test]
        public void Should_ConvertCacheItemPriority_WithHighValue()
        {
            const CacheItemPriority cacheItemPriority = CacheItemPriority.High;

            var obtained = _cacheItemPriorityConverter.Convert(cacheItemPriority);

            obtained.Should().Be(System.Web.Caching.CacheItemPriority.High);
        }

        [Test]
        public void Should_ConvertCacheItemPriority_WithNotRemovableValue()
        {
            const CacheItemPriority cacheItemPriority = CacheItemPriority.NotRemovable;

            var obtained = _cacheItemPriorityConverter.Convert(cacheItemPriority);

            obtained.Should().Be(System.Web.Caching.CacheItemPriority.NotRemovable);
        }
    }
}