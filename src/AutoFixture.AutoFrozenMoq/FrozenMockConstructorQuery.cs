using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using Moq;

namespace AutoFixture.AutoFrozenMoq
{
    public class FrozenMockConstructorQuery : MockConstructorQuery, IMethodQuery
    {
        private readonly IFixture _fixture;

        public FrozenMockConstructorQuery(IFixture fixture)
        {
            _fixture = fixture;
        }

        public new IEnumerable<IMethod> SelectMethods(Type type)
        {
            var selectMethods = base.SelectMethods(type).ToArray();

            if (!selectMethods.Any())
            {
                return Enumerable.Empty<IMethod>();
            }

            if (selectMethods.All(x => x is ConstructorMethod) && IsMock(type))
            {
                return new[] {new FrozenMockRepositoryConstructor(_fixture, GetMockedType(type))};
            }

            return selectMethods;
        }

        internal static bool IsMock(Type type)
        {
            return type != null
                   && type.GetTypeInfo().IsGenericType
                   && typeof(Mock<>).IsAssignableFrom(type.GetGenericTypeDefinition())
                   && !GetMockedType(type).IsGenericParameter;
        }

        internal static Type GetMockedType(Type type)
        {
            return type.GetTypeInfo().GetGenericArguments().Single();
        }
    }
}