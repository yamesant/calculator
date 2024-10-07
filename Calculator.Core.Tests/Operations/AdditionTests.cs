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
    
    [Test]
    [TestCase(12, 4, 16)]
    public void CanEvaluate(double firstSummand, double secondSummand, double expectedResult)
    {
        // Arrange
        List<double> values = [firstSummand, secondSummand];
        Expression expression = Expression.CreateMultiValued(values, new Addition());
        
        // Act
        double result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
}