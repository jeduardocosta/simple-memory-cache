using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SimpleMemoryCache.Entities;

namespace SimpleMemoryCache.UnitTest.Entities
{
    [TestFixture]
    public class CacheItemPolicyTest
    {
        private const string DirectoryStub = @"c:\foo\bar\";

        [Test]
        public void Should_BuildSimplePolicy()
        {
            var policy = new CacheItemPolicy
                .CacheItemPolicyBuilder()
                .Build();

            policy.Should().NotBeNull();
        }

        [Test]
        public void Should_BuildPolicy_WithValidSlidingExpiration()
        {
            var validSlidingExpiration = TimeSpan.FromSeconds(1);
            
            var policy = new CacheItemPolicy
                .CacheItemPolicyBuilder()
                .WithSlidingExpiration(validSlidingExpiration)
                .Build();

            policy.SlidingExpiration.Should().Be(validSlidingExpiration);
        }

        [Test]
        public void Should_BuildPolicy_WithValidAbsolutegExpiration()
        {
            var validAbsolutegExpiration = DateTimeOffset.Now.AddSeconds(1);
            
            var policy = new CacheItemPolicy
                .CacheItemPolicyBuilder()
                .WithAbsoluteExpiration(validAbsolutegExpiration)
                .Build();

            policy.AbsoluteExpiration.Should().Be(validAbsolutegExpiration);
        }

        [Test]
        public void Should_BuildPolicy_WithValidDirectoriesOrFilesChangeMonitor()
        {
            var validDirectoriesOrFilesChangeMonitor = new[] {DirectoryStub};

            var policy = new CacheItemPolicy
                .CacheItemPolicyBuilder()
                .WithDirectoriesOrFilesChangeMonitor(validDirectoriesOrFilesChangeMonitor)
                .Build();

           validDirectoriesOrFilesChangeMonitor
                .SequenceEqual(policy.DirectoriesOrFiles)
                .Should()
                .BeTrue();
        }

        [Test]
        public void Should_BuildPolicy_WithValidCacheItemPriority()
        {
            const CacheItemPriority validCacheItemPriority = CacheItemPriority.High;

            var policy = new CacheItemPolicy
                .CacheItemPolicyBuilder()
                .WithCacheItemPriority(validCacheItemPriority)
                .Build();

            policy.CacheItemPriority.Should().Be(validCacheItemPriority);
        }

        [Test]
        public void Should_BuildPolicy_WithCacheItemPriorityDefaultValue()
        {
            const CacheItemPriority expected = CacheItemPriority.Default;

            var policy = new CacheItemPolicy
                .CacheItemPolicyBuilder()
                .Build();

            policy.CacheItemPriority.Should().Be(expected);
        }
    }
}