namespace Calculator.Core.Tests;

public sealed class Tests
{
    [Test]
    [TestCase(12)]
    public void CanEvaluateSingleValued(double value)
    {
        // Arrange
        var expression = Expression.CreateSingleValued(value);
        
        // Act
        var result = expression.Evaluate();

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void CanEvaluateNested()
    {
        // Arrange
        // ((5 * 4) - 2) / (2 + 3 + 4)  = 2
        var expression = Expression.CreateNested(new List<Expression>()
            {
                Expression.CreateNested(new List<Expression>()
                {
                    Expression.CreateMultiValued(new List<double> { 5, 4 }, new Multiplication()),
                    Expression.CreateSingleValued(2),
                }, new Subtraction()),
                Expression.CreateMultiValued(new List<double> { 2, 3, 4 }, new Addition()),
            },
            new Division());
        var expectedResult = 2;

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
    
    [Test]
    [TestCase(1, 2)]
    public void SameExpressionsEqual(double value1, double value2)
    {
        // Arrange
        Expression expression1 = Expression.CreateMultiValued([value1, value2], new Addition());
        Expression expression2 = Expression.CreateMultiValued([value1, value2], new Addition());
        
        // Assert
        expression1.Should().Be(expression2);
    }
    
    [Test]
    [TestCase(1, 2)]
    public void DifferentValuedExpressionsNotEqual(double value1, double value2)
    {
        // Arrange
        Expression expression1 = Expression.CreateMultiValued([value1, value2], new Addition());
        Expression expression2 = Expression.CreateMultiValued([value2, value1], new Addition());
        
        // Assert
        expression1.Should().NotBe(expression2);
    }
    
    [Test]
    [TestCase(1, 2)]
    public void DifferentOperationExpressionsNotEqual(double value1, double value2)
    {
        // Arrange
        Expression expression1 = Expression.CreateMultiValued([value1, value2], new Addition());
        Expression expression2 = Expression.CreateMultiValued([value1, value2], new Subtraction());
        
        // Assert
        expression1.Should().NotBe(expression2);
    }
}