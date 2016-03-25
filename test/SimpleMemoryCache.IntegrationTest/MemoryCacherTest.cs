using System;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace SimpleMemoryCache.IntegrationTest
{
    [TestFixture]
    public class MemoryCacherTest
    {
        private MemoryCacher _cacher;

        [SetUp]
        public void SetUp()
        {
            _cacher = new MemoryCacher();
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
        public void Should_Set()
        {
            const string key = "thekey";
            const string valueA = "thevalueA";

            _cacher.Set(key, valueA, TimeSpan.FromSeconds(50));
            
            var resultA = _cacher.Retrieve<string>(key);

            resultA.Should().Be(valueA);
        }

        [Test]
        public void Should_Add_AndRetrieveSuccessfully()
        {
            const int expiration = 3;
            const string key = "ShouldAddAndRetrieveSuccessfullyKey";
            const string value = "ShouldAddAndRetrieveSuccessfullyValue";

            _cacher.Add(key, value, TimeSpan.FromSeconds(expiration));
            
            var result = _cacher.Retrieve<string>(key);

            result.Should().Be(value);
        }

        [Test]
        public void Should_Add_WhenInvokingRetrieveOrElse()
        {
            const int expiration = 3;
            const string key = "ShouldAddWhenInvokingRetrieveOrElseKey";
            const string value = "ShouldAddWhenInvokingRetrieveOrElseValue";

            _cacher.RetrieveOrElse(key, TimeSpan.FromSeconds(expiration), () => value);
            
            var result = _cacher.Retrieve<string>(key);

            result.Should().Be(value);
        }

        [Test]
        public void Should_Expire_WhenAdd_WithTimeSpan()
        {
            const int expiration = 3;
            const string key = "ShouldExpireWhenAddWithTimespanKey";
            const string value = "ShouldExpireWhenAddWithTimespanValue";

            _cacher.Add(key, value, TimeSpan.FromMilliseconds(expiration));
            
            Thread.Sleep(expiration * 2);
            
            var result = _cacher.Retrieve<string>(key);

            result.Should().BeNull();
        }

        [Test]
        public void Should_Expire_WhenRetrieveOrElse_WithTimeSpan()
        {
            const int expiration = 2;
            const string key = "ShouldExpireWhenRetrierveOrElseWithTimespanKey";
            const string value = "ShouldExpireWhenRetrierveOrElseWithTimespanValue";

            _cacher.RetrieveOrElse(key, TimeSpan.FromMilliseconds(expiration), () => value);
            
            Thread.Sleep( expiration * 2 );
            
            var result = _cacher.Retrieve<string>(key);

            result.Should().BeNull();
        }

        [Test]
        public void Should_Expire_GivenPolicyWithSlidingExpirationOf_SomeMiliseconds()
        {
            const int someMiliseconds = 2;
            const string key = "ShouldExpire_GivenPolicyWithSlidingExpirationOf_SomeMilisecondsKey";
            const string value = "ShouldExpire_GivenPolicyWithSlidingExpirationOf_SomeMilisecondsValue";

            _cacher.Add(key, value, TimeSpan.FromMilliseconds(someMiliseconds));
            
            Thread.Sleep(someMiliseconds * 2);
            
            var result = _cacher.Retrieve<string>(key);

            result.Should().BeNull();
        }
    }
}