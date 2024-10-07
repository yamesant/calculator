namespace Calculator.Core.Tests.Operations;

public sealed class DivisionTests
{
    [Test]
    public void CanCreate()
    {
        // Arrange
        OperationsInstantiater instantiater = new();
        Division expected = new();
        
        // Act
        Operation operation = instantiater.Create(expected.Name);
        
        // Assert
        operation.Should().Be(expected);
    }
}