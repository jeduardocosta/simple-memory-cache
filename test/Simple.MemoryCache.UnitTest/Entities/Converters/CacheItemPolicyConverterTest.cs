using System;
using FluentAssertions;
using NUnit.Framework;
using Simple.MemoryCache.Converters;
using Simple.MemoryCache.Entities;

namespace Simple.MemoryCache.UnitTest.Entities.Converters
{
    [TestFixture]
    public class CacheItemPolicyConverterTest
    {
        private readonly string _directoryStub = Environment.CurrentDirectory;

        [Test]
        public void Should_InstantiateCacheItemPolicyConverter()
        {
            var converter = new CacheItemPolicyConverter();

            converter.Should().NotBeNull();
        }

        [Test]
        public void Should_ConvertTo_SystemCachingObject_GivenBasicCacheItemPolicy()
        {
            var converter = new CacheItemPolicyConverter();

            var basicCacheItemPolicy = GivenBasicCacheItemPolicy();
            var result = converter.Convert(basicCacheItemPolicy);

            result.Should().NotBeNull();
        }

        [Test]
        public void Should_SetAbsoluteExpiration_GivenCacheItemPolicyWithAboluteExpiration()
        {
            var converter = new CacheItemPolicyConverter();

            var policy = GivenCacheItemPolicyWithAboluteExpiration();
            var result = converter.Convert(policy);

            (result.AbsoluteExpiration == policy.AbsoluteExpiration)
                .Should()
                .BeTrue();
        }

        [Test]
        public void Should_SetAbsoluteExpiration_GivenCacheItemPolicyWithSlidingeExpiration()
        {
            var converter = new CacheItemPolicyConverter();

            var policy = GivenCacheItemPolicyWithSlidingeExpiration();
            var result = converter.Convert(policy);

            (result.SlidingExpiration == policy.SlidingExpiration)
                .Should()
                .BeTrue();
        }

        [Test]
        public void Should_SetAbsoluteExpiration_GivenCacheItemPolicyWithDirectoriesOrFilesChangeMonitor()
        {
            var converter = new CacheItemPolicyConverter();
            var policy = GivenCacheItemPolicyWithDirectoriesOrFilesChangeMonitor();
            var result = converter.Convert(policy);

            result.ChangeMonitors.Count.Should().Be(1);
        }

        private CacheItemPolicy GivenBasicCacheItemPolicy()
        {
            return new CacheItemPolicy
                .CacheItemPolicyBuilder()
                .Build();
        }

        private CacheItemPolicy GivenCacheItemPolicyWithAboluteExpiration()
        {
            return 
                new CacheItemPolicy
                    .CacheItemPolicyBuilder()
                    .WithAbsoluteExpiration(DateTimeOffset.Now.AddSeconds(2))
                    .Build();
        }

        private CacheItemPolicy GivenCacheItemPolicyWithSlidingeExpiration()
        {
            return
                new CacheItemPolicy
                    .CacheItemPolicyBuilder()
                    .WithSlidingExpiration(TimeSpan.FromMinutes(1d))
                    .Build();
        }

        private CacheItemPolicy GivenCacheItemPolicyWithDirectoriesOrFilesChangeMonitor()
        {
            return
                new CacheItemPolicy
                    .CacheItemPolicyBuilder()
                    .WithDirectoriesOrFilesChangeMonitor(new[] { _directoryStub })
                    .Build();
        }
    }
}