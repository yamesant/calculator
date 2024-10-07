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
    
    [Test]
    [TestCase(12, 4, 3)]
    public void CanEvaluate(double dividend, double divisor, double expectedResult)
    {
        // Arrange
        List<double> values = [dividend, divisor];
        Expression expression = Expression.CreateMultiValued(values, new Division());
        
        // Act
        double result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
    
    [Test]
    public void ThrowInvalidArgumentWhenWrongArity()
    {
        // Arrange
        List<double> values = [1, 2, 3];
        
        // Act
        Func<Expression> action = () => Expression.CreateMultiValued(values, new Division());

        // Assert
        action.Should().Throw<ArgumentException>();
    }
}