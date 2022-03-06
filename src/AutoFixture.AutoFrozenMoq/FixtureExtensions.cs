using Moq;

namespace AutoFixture.AutoFrozenMoq
{
    public static class FixtureExtensions
    {
        public static Mock<T> GetFrozenMock<T>(this IFixture fixture) where T : class
        {
            return fixture.Freeze<Mock<T>>();
        }
    }
}