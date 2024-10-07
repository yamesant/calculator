namespace Calculator.Core.Tests.Operations;

public sealed class AdditionTests
{
    [Test]
    public void CanCreate()
    {
        // Arrange
        OperationsInstantiater instantiater = new();
        Addition expected = new();
        
        // Act
        Operation operation = instantiater.Create(expected.Name);
        
        // Assert
        operation.Should().Be(expected);
    }
}