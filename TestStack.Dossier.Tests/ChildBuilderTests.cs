using System;
using Shouldly;
using Xunit;
using TestStack.Dossier.Tests.TestHelpers.Builders;

namespace TestStack.Dossier.Tests
{
    public class ChildBuilderTests
    {
        [Fact]
        public void WhenNotUsingChildBuilder_ThenTheAnonymousValueFixtureIsNotReused()
        {
            var parent = new ParentBuilder().Build();

            parent.ParentEnum.ShouldBe(parent.Child.ChildEnum);
        }

        [Fact]
        public void WhenUsingChildBuilder_ThenTheAnonymousValueFixtureIsReused()
        {
            var parent = new ParentBuilder().WithChildBuilder().Build();

            parent.ParentEnum.ShouldNotBe(parent.Child.ChildEnum);
        }

        [Fact]
        public void WhenUsingChildBuilderIncludingModifier_ThenTheModifierGetsApplied()
        {
            const int number = 2;
            var parent = new ParentBuilder()
                .WithChildBuilder(b => b.WithANumber(number))
                .Build();

            parent.Child.Number.ShouldBe(number);
        }
    }
}
