# AutoFixture.AutoFrozenMoq
An extension for AutoFixture with AutoMoq that creates Mocks as frozen objects

## Project Description
AutoFixture and AutoMoq are designed to teach other and they lessen friciton in the code in case of a change. However, many times you have to manually freeze mocks so you can setup everything easily or verify invocations. The purpose of this library is to lessen the pain with freezing mocks, as when you use this library, all mocks generated by AutoMoq are frozen already. 

## Overview
When you test classes and uses AutoFixture & AutoMoq to do it, many times you have to freeze one or all mocks injected as you want to setup the behaviours when invocation happens. Also, you might want to verify if invocation happened or not. Manually invoking `_fixture.Freeze<Mock<IAmTheInterface>>()` does the trick, but it's an additional work. AutoFixture.AutoFrozenMoq takes that responsibility off of you.

Example:

```c#

public sealed class SomethingTests 
{
	private readonly IFixture _fixture = new Fixture().Customize(new AutoFrozenMoqCustomization());
	
	[Fact]
	public void Test()
	{
		var sut = _fixture.Create<ClassUnderTest>();
		
		sut.Action();
		
		_fixture.GetFrozenMock<IAmTheInterface>().Verify(x => x.(...));
	}
}
```

## Versioning

AutoFixture.AutoFrozenMoq uses [Semantic Versioning 2.0.0](http://semver.org/spec/v2.0.0.html) for the public releases (published to the [nuget.org](https://www.nuget.org/)).

## Additional resources

* [Rafal Kozlowski's blog](https://rafalkozlowski.engineer)