namespace Calculator.Core.Tests.Operations;

public sealed class MultiplicationTests
{
    [Test]
    public void CanCreate()
    {
        // Arrange
        OperationsInstantiater instantiater = new();
        Multiplication expected = new();
        
        // Act
        Operation operation = instantiater.Create(expected.Name);
        
        // Assert
        operation.Should().Be(expected);
    }
}