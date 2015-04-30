using System;
using System.Threading;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Simple.MemoryCache.Converters;
using Simple.MemoryCache.Entities;

namespace Simple.MemoryCache.UnitTest
{
    [TestFixture]
    public class MemoryCacherTest
    {
        private MemoryCacher _cacher;

        private Mock<ICacheItemPolicyConverter> _cacheItemPolicyConverterMock;
        private Mock<ICacheItemPriorityConverter> _cacheItemPriorityMock;
         
        [SetUp]
        public void Init()
        {
            _cacheItemPolicyConverterMock = new Mock<ICacheItemPolicyConverter>();
            _cacheItemPolicyConverterMock
                .Setup(e => e.Convert(It.IsAny<CacheItemPolicy>()))
                .Returns((System.Runtime.Caching.CacheItemPolicy)null);

            _cacheItemPriorityMock = new Mock<ICacheItemPriorityConverter>();
            _cacheItemPriorityMock
                .Setup(e => e.Convert(It.IsAny<CacheItemPriority>()))
                .Returns(System.Web.Caching.CacheItemPriority.Default);

            _cacher = new MemoryCacher(_cacheItemPolicyConverterMock.Object, _cacheItemPriorityMock.Object);
        }

        [Test]
        public void Should_AddWithoutErrors()
        {
            _cacher.Add("ShouldAddWithoutErrorsKey", "ShouldAddWithoutErrorsValue", TimeSpan.FromSeconds(1));
        }

        [Test]
        public void Should_RetrieveNull_WhenValueDoesntExist()
        {
            var result = _cacher.Retrieve<string>("ShouldRetrieveNullWhenValueDoesntExistKey");

            result.Should().BeNull();
        }

        [Test]
        public void Should_Add_AndRetrieveSuccessfully()
        {
            const string key = "ShouldAddAndRetrieveSuccessfullyKey";
            const string value = "ShouldAddAndRetrieveSuccessfullyValue";
            _cacher.Add(key, value, TimeSpan.FromSeconds(1));
            
            var result = _cacher.Retrieve<string>(key);

            result.Should().Be(value);
        }

        [Test]
        public void Should_Add_WhenInvokingRetrieveOrElse()
        {
            const string key = "ShouldAddWhenInvokingRetrieveOrElseKey";
            const string value = "ShouldAddWhenInvokingRetrieveOrElseValue";
            
            _cacher.RetrieveOrElse(key, TimeSpan.FromSeconds(1), () => value);
            
            var result = _cacher.Retrieve<string>(key);

            result.Should().Be(value);
        }

        [Test]
        public void Should_Expire_WhenAdd_WithTimeSpan()
        {
            const string key = "ShouldExpireWhenAddWithTimespanKey";
            const string value = "ShouldExpireWhenAddWithTimespanValue";
            
            _cacher.Add(key, value, TimeSpan.FromMilliseconds(3));
           
            Thread.Sleep(5);
            
            var result = _cacher.Retrieve<string>(key);

            result.Should().BeNull();
        }

        [Test]
        public void Should_Expire_WhenRetrieveOrElse_WithTimeSpan()
        {
            const string key = "ShouldExpireWhenRetrierveOrElseWithTimespanKey";
            const string value = "ShouldExpireWhenRetrierveOrElseWithTimespanValue";
            
            var expiration = TimeSpan.FromMilliseconds(2);
            GivenPolicyWithSlidingExpirationOf(expiration);
            _cacher.RetrieveOrElse(key, expiration, () => value);
            
            Thread.Sleep(7);
            
            var result = _cacher.Retrieve<string>(key);

            result.Should().BeNull();
        }

        [Test]
        public void Should_Expire_GivenPolicyWithSlidingExpirationOf_SomeMiliseconds()
        {
            const int someMiliseconds = 2;

            var policy = new CacheItemPolicy.CacheItemPolicyBuilder()
                .WithSlidingExpiration(TimeSpan.FromMilliseconds(someMiliseconds))
                .Build();
            
            const string key = "ShouldExpire_GivenPolicyWithSlidingExpirationOf_SomeMilisecondsKey";
            const string value = "ShouldExpire_GivenPolicyWithSlidingExpirationOf_SomeMilisecondsValue";
            
            _cacher.Add(key, value, policy);
            Thread.Sleep(5);
            
            var result = _cacher.Retrieve<string>(key);

            result.Should().BeNull();
        }

        [Test]
        public void ShouldNot_ConvertWhenAddWithTimespan()
        {
            _cacher.Add("ShouldNotConvertWhenAddWithTimespanKey", "ShouldNotConvertWhenAddWithTimespanValue", TimeSpan.FromSeconds(1));
            _cacheItemPolicyConverterMock
                .Verify(e => e.Convert(It.IsAny<CacheItemPolicy>()), Times.Never);
        }

        [Test]
        public void Should_UseCacheItemPriorityConverter_WhenAddSomething()
        {
            var policy = new CacheItemPolicy.CacheItemPolicyBuilder()
                .WithCacheItemPriority(CacheItemPriority.Normal)
                .Build();

            _cacher.Add("ShouldUseCacheItemPriorityConverterWhenAddSomething",
                "ShouldUseCacheItemPriorityConverterWhenAddSomethingValue", 
                policy);

            _cacheItemPriorityMock
                .Verify(e => e.Convert(It.IsAny<CacheItemPriority>()), Times.Once);
        }

        [Test]
        public void Should_UseCacheItemPriorityConverter_WhenAddSomething_AndHasNotCacheItemPriorityValue()
        {
            const int someMiliseconds = 2;

            var policy = new CacheItemPolicy.CacheItemPolicyBuilder()
                .WithSlidingExpiration(TimeSpan.FromMilliseconds(someMiliseconds))
                .Build();

            _cacher.Add("ShouldUseCacheItemPriorityConverterWhenAddSomethingAndHasNotCacheItemPriorityValue",
                "ShouldUseCacheItemPriorityConverterWhenAddSomethingAndHasNotCacheItemPriorityValue",
                policy);

            _cacheItemPriorityMock
                .Verify(e => e.Convert(It.IsAny<CacheItemPriority>()), Times.Once);
        }
        
        private void GivenPolicyWithSlidingExpirationOf(TimeSpan expiration)
        {
            var policy = new System.Runtime.Caching.CacheItemPolicy { SlidingExpiration = expiration };

            _cacheItemPolicyConverterMock = new Mock<ICacheItemPolicyConverter>();
            _cacheItemPolicyConverterMock
                .Setup(e => e.Convert(It.IsAny<CacheItemPolicy>()))
                .Returns(policy);

            _cacher = new MemoryCacher(_cacheItemPolicyConverterMock.Object, _cacheItemPriorityMock.Object);
        }
    }
}