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
    
    [Test]
    [TestCase(12, 4, 8)]
    public void CanEvaluate(double minuend, double subtrahend, double expectedResult)
    {
        // Arrange
        List<double> values = [minuend, subtrahend];
        Expression expression = Expression.CreateMultiValued(values, new Subtraction());
        
        // Act
        double result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
}