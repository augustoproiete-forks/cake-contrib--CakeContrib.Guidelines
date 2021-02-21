using System.Linq;

using CakeContrib.Guidelines.Tasks.Tests.Fixtures;

using FluentAssertions;

using Xunit;

namespace CakeContrib.Guidelines.Tasks.Tests
{
    public class TargetFrameworkVersionsTests
    {
        private const string NetStandard20 = "netstandard2.0";
        private const string Net46 = "net46";
        private const string Net461 = "net461";
        private const string Net50 = "net5.0";

        [Fact]
        public void Should_Error_If_RequiredTargetFramework_Is_Not_Targeted()
        {
            // given
            var fixture = new TargetFrameworkVersionsFixture();

            // when
            fixture.Execute();

            // then
            fixture.BuildEngine.ErrorEvents.Should().HaveCount(1);
            fixture.BuildEngine.ErrorEvents.First().Message.Should().Contain(NetStandard20);
        }

        [Fact]
        public void Should_Not_Error_If_RequiredTargetFramework_Is_Not_Targeted_But_Omitted()
        {
            // given
            var fixture = new TargetFrameworkVersionsFixture();
            fixture.WithOmittedTargetFramework(NetStandard20);

            // when
            fixture.Execute();

            // then
            fixture.BuildEngine.ErrorEvents.Should().HaveCount(0);
        }

        [Fact]
        public void Should_Warn_If_SuggestedTargetFramework_Is_Not_Targeted()
        {
            // given
            var fixture = new TargetFrameworkVersionsFixture();
            fixture.WithTargetFramework(NetStandard20);

            // when
            fixture.Execute();

            // then
            fixture.BuildEngine.ErrorEvents.Should().HaveCount(0);
            fixture.BuildEngine.WarningEvents.Should().HaveCount(1);
            fixture.BuildEngine.WarningEvents.First().Message.Should().Contain(Net46);
        }

        [Fact]
        public void Should_Not_Warn_If_SuggestedTargetFramework_Is_Not_Targeted_But_Omitted()
        {
            // given
            var fixture = new TargetFrameworkVersionsFixture();
            fixture.WithTargetFramework(NetStandard20);
            fixture.WithOmittedTargetFramework(Net461);

            // when
            fixture.Execute();

            // then
            fixture.BuildEngine.ErrorEvents.Should().HaveCount(0);
            fixture.BuildEngine.WarningEvents.Should().HaveCount(0);
        }

        [Fact]
        public void Should_Not_Warn_If_Required_And_SuggestedTargetFramework_Is_Targeted()
        {
            // given
            var fixture = new TargetFrameworkVersionsFixture();
            fixture.WithTargetFrameworks(NetStandard20, Net461);

            // when
            fixture.Execute();

            // then
            fixture.BuildEngine.ErrorEvents.Should().HaveCount(0);
            fixture.BuildEngine.WarningEvents.Should().HaveCount(0);
        }

        [Fact]
        public void Should_Not_Warn_If_Required_And_Alternative_SuggestedTargetFramework_Is_Targeted()
        {
            // given
            var fixture = new TargetFrameworkVersionsFixture();
            fixture.WithTargetFrameworks(NetStandard20, Net46);

            // when
            fixture.Execute();

            // then
            fixture.BuildEngine.ErrorEvents.Should().HaveCount(0);
            fixture.BuildEngine.WarningEvents.Should().HaveCount(0);
        }

        [Fact]
        public void Should_Warn_If_Net5_is_missing_and_Cake_Version_is_1_0_0()
        {
            // given
            var fixture = new TargetFrameworkVersionsFixture();
            fixture.WithCakeCoreReference(1);
            fixture.WithTargetFrameworks(NetStandard20, Net461);

            // when
            fixture.Execute();

            // then
            fixture.BuildEngine.ErrorEvents.Should().HaveCount(0);
            fixture.BuildEngine.WarningEvents.Should().HaveCount(1);
            fixture.BuildEngine.WarningEvents.First().Message.Should().Contain(Net50);
        }

        [Fact]
        public void Should_Not_Warn_If_All_References_for_Cake_Version_is_1_0_0_is_present()
        {
            // given
            var fixture = new TargetFrameworkVersionsFixture();
            fixture.WithCakeCoreReference(1);
            fixture.WithTargetFrameworks(NetStandard20, Net461, Net50);

            // when
            fixture.Execute();

            // then
            fixture.BuildEngine.ErrorEvents.Should().HaveCount(0);
            fixture.BuildEngine.WarningEvents.Should().HaveCount(0);
        }
    }
}
