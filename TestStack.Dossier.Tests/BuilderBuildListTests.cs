﻿using System.Collections.Generic;
using System.Linq;
using Shouldly;
using TestStack.Dossier.DataSources.Generators;
using TestStack.Dossier.Lists;
using TestStack.Dossier.Tests.Builders;
using TestStack.Dossier.Tests.Entities;
using Xunit;

namespace TestStack.Dossier.Tests
{
    public class BuilderBuildListTests
    {
        [Fact]
        public void GivenANormalBuilderInstance_WhenCallingIsListBuilderProxy_ThenReturnFalse()
        {
            var builder = Builder<Customer>.CreateNew();

            builder.IsListBuilderProxy().ShouldBe(false);
        }

        [Fact]
        public void GivenAListBuilderProxyInstance_WhenCallingIsListBuilderProxy_ThenReturnTrue()
        {
            var builder = Builder<Customer>.CreateListOfSize(1).TheFirst(1);

            builder.IsListBuilderProxy().ShouldBe(true);
        }

        [Fact]
        public void GivenListOfBuilders_WhenCallingBuildListExplicitly_ThenAListOfEntitiesOfTheRightSizeShouldBeReturned()
        {
            var builders = Builder<Customer>.CreateListOfSize(5);

            var entities = builders.BuildList();

            entities.Count.ShouldBe(5);
        }

        [Fact]
        public void GivenListOfBuilders_WhenCallingBuildListImplicitly_ThenAListOfEntitiesOfTheRightSizeShouldBeReturned()
        {
            List<Customer> entities = Builder<Customer>.CreateListOfSize(5);

            entities.Count.ShouldBe(5);
        }

        [Fact]
        public void GivenListOfBuilders_WhenCallingBuildListExplicitly_ThenAListOfEntitiesOfTheRightTypeShouldBeReturned()
        {
            var builders = Builder<Customer>.CreateListOfSize(5);

            var entities = builders.BuildList();

            entities.ShouldBeAssignableTo<IList<Customer>>();
        }

        [Fact]
        public void GivenListOfBuilders_WhenCallingBuildListImplicitly_ThenAListOfEntitiesOfTheRightTypeShouldBeReturned()
        {
            List<Customer> entities = Builder<Customer>.CreateListOfSize(5);

            entities.ShouldBeAssignableTo<IList<Customer>>();
        }

        [Fact]
        public void GivenListOfBuilders_WhenCallingBuildListExplicitly_ThenAListOfUniqueEntitiesShouldBeReturned()
        {
            var builders = Builder<Customer>.CreateListOfSize(5);

            var entities = builders.BuildList();

            entities[0].ShouldNotBe(entities[1]);
            entities[0].ShouldNotBe(entities[2]);
            entities[0].ShouldNotBe(entities[3]);
            entities[0].ShouldNotBe(entities[4]);
            entities[1].ShouldNotBe(entities[2]);
            entities[1].ShouldNotBe(entities[3]);
            entities[1].ShouldNotBe(entities[4]);
            entities[2].ShouldNotBe(entities[3]);
            entities[2].ShouldNotBe(entities[4]);
            entities[3].ShouldNotBe(entities[4]);
        }

        [Fact]
        public void GivenListOfBuilders_WhenCallingBuildListImplicitly_ThenAListOfUniqueEntitiesShouldBeReturned()
        {
            List<Customer> entities = Builder<Customer>.CreateListOfSize(5);

            entities[0].ShouldNotBe(entities[1]);
            entities[0].ShouldNotBe(entities[2]);
            entities[0].ShouldNotBe(entities[3]);
            entities[0].ShouldNotBe(entities[4]);
            entities[1].ShouldNotBe(entities[2]);
            entities[1].ShouldNotBe(entities[3]);
            entities[1].ShouldNotBe(entities[4]);
            entities[2].ShouldNotBe(entities[3]);
            entities[2].ShouldNotBe(entities[4]);
            entities[3].ShouldNotBe(entities[4]);
        }

        [Fact]
        public void GivenListOfBuildersWithCustomisation_WhenBuildingEntitiesExplicitly_ThenTheCustomisationShouldTakeEffect()
        {
            var generator = new SequentialGenerator(0, 100);
            var list = CustomerBuilder.CreateListOfSize(3)
                .All().With(b => b.WithFirstName(generator.Generate().ToString()));

            var data = list.BuildList();

            data.Select(c => c.FirstName).ToArray()
                .ShouldBe(new[] { "0", "1", "2" });
        }

        [Fact]
        public void GivenListOfBuildersWithCustomisation_WhenBuildingEntitiesImplicitly_ThenTheCustomisationShouldTakeEffect()
        {
            var generator = new SequentialGenerator(0, 100);

            List<Customer> data = CustomerBuilder.CreateListOfSize(3)
                .All().With(b => b.WithFirstName(generator.Generate().ToString()));

            data.Select(c => c.FirstName).ToArray()
                .ShouldBe(new[] { "0", "1", "2" });
        }

        [Fact]
        public void GivenListOfBuildersWithARangeOfCustomisationMethods_WhenBuildingEntitiesExplicitly_ThenThenTheListIsBuiltAndModifiedCorrectly()
        {
            var i = 0;
            var customers = CustomerBuilder.CreateListOfSize(5)
                .TheFirst(1).WithFirstName("First")
                .TheNext(1).WithLastName("Next Last")
                .TheLast(1).WithLastName("Last Last")
                .ThePrevious(2).With(b => b.WithLastName("last" + (++i).ToString()))
                .All().WhoJoinedIn(1999)
                .BuildList();

            customers.ShouldBeAssignableTo<IList<Customer>>();
            customers.Count.ShouldBe(5);
            customers[0].FirstName.ShouldBe("First");
            customers[1].LastName.ShouldBe("Next Last");
            customers[2].LastName.ShouldBe("last1");
            customers[3].LastName.ShouldBe("last2");
            customers[4].LastName.ShouldBe("Last Last");
            customers.ShouldAllBe(c => c.YearJoined == 1999);
        }

        [Fact]
        public void GivenListOfBuildersWithARangeOfCustomisationMethods_WhenBuildingEntitiesImplicitly_ThenThenTheListIsBuiltAndModifiedCorrectly()
        {
            var i = 0;
            List<Customer> customers = CustomerBuilder.CreateListOfSize(5)
                .TheFirst(1).WithFirstName("First")
                .TheNext(1).WithLastName("Next Last")
                .TheLast(1).WithLastName("Last Last")
                .ThePrevious(2).With(b => b.WithLastName("last" + (++i).ToString()))
                .All().WhoJoinedIn(1999);

            customers.ShouldBeAssignableTo<IList<Customer>>();
            customers.Count.ShouldBe(5);
            customers[0].FirstName.ShouldBe("First");
            customers[1].LastName.ShouldBe("Next Last");
            customers[2].LastName.ShouldBe("last1");
            customers[3].LastName.ShouldBe("last2");
            customers[4].LastName.ShouldBe("Last Last");
            customers.ShouldAllBe(c => c.YearJoined == 1999);
        }

        [Fact]
        public void WhenBuildingEntitiesExplicitly_ThenTheAnonymousValueFixtureIsSharedAcrossBuilders()
        {
            var customers = CustomerBuilder.CreateListOfSize(5).BuildList();

            customers[0].CustomerClass.ShouldBe(CustomerClass.Normal);
            customers[1].CustomerClass.ShouldBe(CustomerClass.Bronze);
            customers[2].CustomerClass.ShouldBe(CustomerClass.Silver);
            customers[3].CustomerClass.ShouldBe(CustomerClass.Gold);
            customers[4].CustomerClass.ShouldBe(CustomerClass.Platinum);
        }

        [Fact]
        public void WhenBuildingEntitiesImplicitly_ThenTheAnonymousValueFixtureIsSharedAcrossBuilders()
        {
            List<Customer> customers = CustomerBuilder.CreateListOfSize(5);

            customers[0].CustomerClass.ShouldBe(CustomerClass.Normal);
            customers[1].CustomerClass.ShouldBe(CustomerClass.Bronze);
            customers[2].CustomerClass.ShouldBe(CustomerClass.Silver);
            customers[3].CustomerClass.ShouldBe(CustomerClass.Gold);
            customers[4].CustomerClass.ShouldBe(CustomerClass.Platinum);
        }
    }
}