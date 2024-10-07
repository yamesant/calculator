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
    
    [Test]
    [TestCase(12, 4, 48)]
    public void CanEvaluate(double firstFactor, double secondFactor, double expectedResult)
    {
        // Arrange
        List<double> values = [firstFactor, secondFactor];
        Expression expression = Expression.CreateMultiValued(values, new Multiplication());
        
        // Act
        double result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
}