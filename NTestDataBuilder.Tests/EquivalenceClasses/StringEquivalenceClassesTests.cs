﻿using NTestDataBuilder.EquivalenceClasses;
using Shouldly;
using Xunit;

namespace NTestDataBuilder.Tests.EquivalenceClasses
{
    public class StringEquivalenceClassesTests
    {
        public AnonymousValueFixture Any { get; private set; }

        public StringEquivalenceClassesTests()
        {
            Any = new AnonymousValueFixture();
        }

        [Fact]
        public void WhenGettingAnyString_ThenReturnDifferentNonEmptyStringsEveryTime()
        {
            var s1 = Any.String();
            var s2 = Any.String();

            s1.ShouldBeOfType<string>();
            s2.ShouldBeOfType<string>();
            s1.ShouldNotBeNullOrEmpty();
            s2.ShouldNotBeNullOrEmpty();
            s1.ShouldNotBe(s2);
        }

        [Fact]
        public void WhenGettingAnyStringStartingWithSomething_ThenReturnDifferentStringsStartingWithThatStringEveryTime()
        {
            var s1 = Any.StringStartingWith("St4rt");
            var s2 = Any.StringStartingWith("St4rt");
            var s3 = Any.StringStartingWith("Something Else");

            s1.ShouldNotBe(s2);
            s1.ShouldNotBe(s3);
            s2.ShouldNotBe(s3);
            s1.ShouldStartWith("St4rt");
            s2.ShouldStartWith("St4rt");
            s3.ShouldStartWith("Something Else");
        }
    }
}
