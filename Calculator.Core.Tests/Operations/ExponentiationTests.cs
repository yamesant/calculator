namespace Calculator.Core.Tests.Operations;

public sealed class ExponentiationTests
{
    [Test]
    public void CanCreate()
    {
        // Arrange
        OperationsInstantiater instantiater = new();
        Exponentiation expected = new();
        
        // Act
        Operation operation = instantiater.Create(expected.Name);
        
        // Assert
        operation.Should().Be(expected);
    }
    
    [Test]
    [TestCase(0, 1)]
    [TestCase(1, double.E)]
    public void CanEvaluate(double value, double expectedResult)
    {
        // Arrange
        List<double> values = [value];
        Expression expression = Expression.CreateMultiValued(values, new Exponentiation());
        
        // Act
        double result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
}