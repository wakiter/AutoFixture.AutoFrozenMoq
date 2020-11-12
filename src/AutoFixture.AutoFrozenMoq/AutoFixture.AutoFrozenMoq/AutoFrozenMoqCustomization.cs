using System;
using System.Linq;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;

namespace AutoFixture.AutoFrozenMoq
{
    public class AutoFrozenMoqCustomization : ICustomization
    {
        private readonly ISpecimenBuilder _relay = new MockRelay();

        public void Customize(IFixture fixture)
        {
            if (fixture == null)
            {
                throw new ArgumentNullException(nameof(fixture));
            }

            var mockBuilder = new MockPostprocessor(new MethodInvoker(new FrozenMockConstructorQuery(fixture)));

            fixture.Customizations.Add(mockBuilder);

            if (!fixture.ResidueCollectors.Any(x => x is MockRelay))
            {
                fixture.ResidueCollectors.Add(_relay);
            }
        }
    }
}
