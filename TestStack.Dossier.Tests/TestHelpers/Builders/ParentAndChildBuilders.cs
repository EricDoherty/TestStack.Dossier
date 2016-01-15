using System;
using System.Collections.Generic;
using TestStack.Dossier.Lists;

namespace TestStack.Dossier.Tests.TestHelpers.Builders
{
	public enum AnEnum
	{
		One,
		Two
	}

	public class ParentObject
	{
		public ParentObject(AnEnum parentEnum, ChildObject child, List<ChildObject> children)
		{
			ParentEnum = parentEnum;
			Child = child;
			Children = children;
		}

		public AnEnum ParentEnum { get; private set; }
		public ChildObject Child { get; private set; }
		public List<ChildObject> Children { get; private set; }
	}

	public class ChildObject
	{
		public ChildObject(AnEnum childEnum, int number)
		{
			ChildEnum = childEnum;
			Number = number;
		}

		public AnEnum ChildEnum { get; private set; }
		public int Number { get; private set; }
	}

	public class ParentBuilder : TestDataBuilder<ParentObject, ParentBuilder>
	{
		public ParentBuilder()
		{
			Set(x => x.Child, new ChildBuilder());
			Set(x => x.Children, ChildBuilder.CreateListOfSize(0));
		}

		public ParentBuilder WithChildBuilder(Func<ChildBuilder, ChildBuilder> modifier = null)
		{
			return Set(x => x.Child, GetChildBuilder<ChildObject, ChildBuilder>(modifier));
		}

		public virtual ParentBuilder WithChildrenBuilder(int listSize, Func<ListBuilder<ChildObject, ChildBuilder>, ChildBuilder> modifier = null)
		{
			return Set(x => x.Children, GetListChildBuilder<ChildObject, ChildBuilder, ListBuilder<ChildObject, ChildBuilder>>(() => ChildBuilder.CreateListOfSize(listSize), modifier));
		}

		protected override ParentObject BuildObject()
		{
			return new ParentObject(Get(x => x.ParentEnum), Get(x => x.Child), Get(x => x.Children));
		}
	}

	public class ChildBuilder : TestDataBuilder<ChildObject, ChildBuilder>
	{
		public ChildBuilder()
		{
			WithANumber(1);
		}

		public virtual ChildBuilder WithANumber(int number)
		{
			return Set(x => x.Number, number);
		}

		protected override ChildObject BuildObject()
		{
			return new ChildObject(Get(x => x.ChildEnum), Get(x => x.Number));
		}
	}
}