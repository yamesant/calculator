namespace Calculator.Core.Tests.Operations;

public sealed class DirectionTests
{
    [Test]
    public void CanCreate()
    {
        // Arrange
        OperationsInstantiater instantiater = new();
        Direction expected = new();
        
        // Act
        Operation operation = instantiater.Create(expected.Name);
        
        // Assert
        operation.Should().Be(expected);
    }
    
    [Test]
    [TestCase(0, 0, 0)]
    [TestCase(1, 0, -1)]
    [TestCase(0, 1, 1)]
    public void CanEvaluate(double x, double y, double expectedResult)
    {
        // Arrange
        List<double> values = [x, y];
        Expression expression = Expression.CreateMultiValued(values, new Direction());
        
        // Act
        double result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
}