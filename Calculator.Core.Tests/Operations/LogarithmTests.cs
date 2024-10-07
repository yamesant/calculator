namespace Calculator.Core.Tests.Operations;

public sealed class LogarithmTests
{
    [Test]
    public void CanCreate()
    {
        // Arrange
        OperationsInstantiater instantiater = new();
        Logarithm expected = new();
        
        // Act
        Operation operation = instantiater.Create(expected.Name);
        
        // Assert
        operation.Should().Be(expected);
    }
    
    [Test]
    [TestCase(1, 0)]
    [TestCase(double.E, 1)]
    public void CanEvaluate(double value, double expectedResult)
    {
        // Arrange
        List<double> values = [value];
        Expression expression = Expression.CreateMultiValued(values, new Logarithm());
        
        // Act
        double result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
}