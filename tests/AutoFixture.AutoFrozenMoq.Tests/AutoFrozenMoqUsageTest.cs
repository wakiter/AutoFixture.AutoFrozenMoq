using System;
using Moq;
using Xunit;

namespace AutoFixture.AutoFrozenMoq.Tests
{
    public sealed class AutoFrozenMoqUsageTest
    {
        [Fact]
        public void AutoFrozen_interface_is_frozen_and_returned_when_needed()
        {
            var fixture = new Fixture().Customize(new AutoFrozenMoqCustomization());

            var sut = fixture.Create<DummyClass>();

            sut.SomeAction();

            fixture.GetFrozenMock<IAmInjectedInterface>().Verify(x => x.SomeIntefaceAction(), Times.Once);
        }

        public class DummyClass
        {
            public IAmInjectedInterface InjectedInterface { get; }

            public DummyClass(IAmInjectedInterface injectedInterface)
            {
                InjectedInterface = injectedInterface;
            }

            public void SomeAction()
            {
                InjectedInterface.SomeIntefaceAction();
            }
        }

        public interface IAmInjectedInterface
        {
            Guid Id { get; }

            void SomeIntefaceAction();
        }
    }
}