namespace Calculator.Core.Tests.Operations;

public sealed class SubtractionTests
{
    [Test]
    public void CanCreate()
    {
        // Arrange
        OperationsInstantiater instantiater = new();
        Subtraction expected = new();
        
        // Act
        Operation operation = instantiater.Create(expected.Name);
        
        // Assert
        operation.Should().Be(expected);
    }   
}