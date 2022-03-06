using System;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using FluentAssertions;
using Moq;
using Xunit;

namespace AutoFixture.AutoFrozenMoq.Tests
{
    public sealed class AutoFrozenMoqCustomizationTests
    {
        [Fact]
        public void Customize_Adds_Customization_And_ResidueCollectors()
        {
            var fixture = new Fixture();
            var sut = new AutoFrozenMoqCustomization();

            var actual = fixture.Customize(sut);

            actual.ResidueCollectors.Should().Contain(x => x is MockRelay);
            actual.Customizations.Should().Contain(x => x is MockPostprocessor
                                                        && ((MockPostprocessor)x).Builder is MethodInvoker 
                                                        && ((MethodInvoker)((MockPostprocessor)x).Builder).Query is FrozenMockConstructorQuery);
        }

        [Fact]
        public void MockRepository_Is_Injected_When_Dependency_Resolved_And_This_Repository_Contains_Frozen_Mock()
        {
            var sut = new Fixture().Customize(new AutoFrozenMoqCustomization());

            sut.Create<DummyClass>();

            var mockRepository = sut.Freeze<MockRepository>();
            mockRepository.Should().NotBeNull();
            mockRepository.Should().BeOfType<FrozenMockRepository>();
            ((FrozenMockRepository) mockRepository).GetExisting<IAmInjectedInterface>().Should().NotBeNull();
        }

        [Fact]
        public void MockRepository_Is_Injected_When_Dependency_Resolved_And_It_Is_Frozen()
        {
            var sut = new Fixture().Customize(new AutoFrozenMoqCustomization());

            var createdObject = sut.Create<DummyClass>();

            var actual = sut.Freeze<Mock<IAmInjectedInterface>>();
            actual.Should().NotBeNull();
            actual.Object.Should().Be(createdObject.InjectedInterface);
        }

        [Fact]
        public void MockRepository_Is_Injecteed_When_Dependency_Resolved_And_Dependency_Is_Frozen()
        {
            var sut = new Fixture().Customize(new AutoFrozenMoqCustomization());

            var createdObject = sut.Create<DummyClass>();

            var actual = sut.Create<Mock<IAmInjectedInterface>>();

            actual.Object.Should().Be(createdObject.InjectedInterface);

            actual = sut.Create<Mock<IAmInjectedInterface>>();

            actual.Object.Should().Be(createdObject.InjectedInterface);
        }

        public class DummyClass
        {
            public IAmInjectedInterface InjectedInterface { get; }

            public DummyClass(IAmInjectedInterface injectedInterface)
            {
                InjectedInterface = injectedInterface;
            }
        }

        public interface IAmInjectedInterface
        {
            Guid Id { get; }
        }
    }
}
