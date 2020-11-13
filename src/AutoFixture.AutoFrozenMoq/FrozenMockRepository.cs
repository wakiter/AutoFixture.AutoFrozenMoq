using System.Linq;
using System.Runtime.CompilerServices;
using Moq;

[assembly:InternalsVisibleTo("AutoFixture.AutoFrozenMoq.Tests")]
namespace AutoFixture.AutoFrozenMoq
{
    internal sealed class FrozenMockRepository : MockRepository
    {
        public Mock<T> GetExisting<T>()
            where T : class
        {
            var mock = Mocks.FirstOrDefault(x => x is Mock<T>);
            return mock as Mock<T>;
        }

        public FrozenMockRepository()
            : base(MockBehavior.Loose)
        {
        }
    }
}