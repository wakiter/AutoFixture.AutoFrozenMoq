using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoFixture.Kernel;
using Moq;

namespace AutoFixture.AutoFrozenMoq
{
    internal sealed class FrozenMockRepositoryConstructor : IMethod
    {
        public IEnumerable<ParameterInfo> Parameters => Enumerable.Empty<ParameterInfo>();

        private readonly IFixture _fixture;
        private readonly Type _type;

        public FrozenMockRepositoryConstructor(IFixture fixture, Type type)
        {
            _fixture = fixture;
            _type = type;
        }

        public object Invoke(IEnumerable<object> parameters)
        {
            var mockRepositoryType = typeof(FrozenMockRepository);
            var mockRepository = _fixture.Freeze<FrozenMockRepository>();
            _fixture.Inject<MockRepository>(mockRepository);
            
            var mock = mockRepositoryType
                .GetMethod(nameof(FrozenMockRepository.GetExisting), BindingFlags.Public | BindingFlags.Instance)
                .MakeGenericMethod(_type)
                .Invoke(mockRepository, new object[0]);

            if (mock == null)
            {
                mock = mockRepositoryType
                    .GetMethod(nameof(FrozenMockRepository.Create), Type.EmptyTypes)
                    .MakeGenericMethod(_type)
                    .Invoke(mockRepository, new object[0]);
            }

            return mock;
        }
    }
}