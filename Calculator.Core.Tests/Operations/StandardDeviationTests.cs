namespace Calculator.Core.Tests.Operations;

public sealed class StandardDeviationTests
{
    [Test]
    public void CanCreate()
    {
        // Arrange
        OperationsInstantiater instantiater = new();
        StandardDeviation expected = new();
        
        // Act
        Operation operation = instantiater.Create(expected.Name);
        
        // Assert
        operation.Should().Be(expected);
    }
    
    [Test]
    [TestCase(new double[] { }, 0)]
    [TestCase(new double[] { 1 }, 0)]
    [TestCase(new double[] { 1, 1 }, 0)]
    [TestCase(new double[] { 4, 2 }, 1)]
    [TestCase(new double[] { 1, 3, 9, 19 }, 7)]
    public void CanEvaluate(double[] values, double expectedResult)
    {
        // Arrange
        Expression expression = Expression.CreateMultiValued(values.ToList(), new StandardDeviation());
        
        // Act
        double result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
}