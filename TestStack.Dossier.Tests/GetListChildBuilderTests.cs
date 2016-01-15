using System.Linq;
using Shouldly;
using TestStack.Dossier.Tests.TestHelpers.Builders;
using Xunit;

namespace TestStack.Dossier.Tests
{
	public class GetListChildBuilderTests
	{
		[Fact]
		public void WhenUsingChildListBuilderWithoutModifier_ThenAListOfDefaultChildrenObjectsGetCreated()
		{
			var parent = new ParentBuilder()
				.WithChildrenBuilder(4)
				.Build();

			parent.Children.Count.ShouldBe(4);
		}

		[Fact]
		public void WhenUsingChildListBuilderIncludingModifier_ThenTheModifierGetsApplied()
		{
			const int number = 2;
			var parent = new ParentBuilder()
				.WithChildrenBuilder(4, b => b.All().WithANumber(number))
				.Build();

			parent.Children.Count.ShouldBe(4);
			parent.Children.ForEach((x) => x.Number.ShouldBe(2));
		}
	}
}
