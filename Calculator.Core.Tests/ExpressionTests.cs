using FluentAssertions;

namespace Calculator.Core.Tests;

public sealed class Tests
{
    [Test]
    [TestCase(12)]
    public void SingleValue(double value)
    {
        // Arrange
        var expression = Expression.CreateSingleValued(value);
        
        // Act
        var result = expression.Evaluate();

        // Assert
        result.Should().Be(value);
    }
    
    [Test]
    [TestCase(12, 4, 16)]
    public void Addition(double firstSummand, double secondSummand, double expectedResult)
    {
        // Arrange
        var values = new List<double> { firstSummand, secondSummand };
        var expression = Expression.CreateMultiValued(values, new Addition());
        
        // Act
        var result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
    
    [Test]
    [TestCase(12, 4, 48)]
    public void Multiplication(double firstFactor, double secondFactor, double expectedResult)
    {
        // Arrange
        var values = new List<double> { firstFactor, secondFactor };
        var expression = Expression.CreateMultiValued(values, new Multiplication());
        
        // Act
        var result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
    
    [Test]
    [TestCase(12, 4, 3)]
    public void Division(double dividend, double divisor, double expectedResult)
    {
        // Arrange
        var values = new List<double> { dividend, divisor };
        var expression = Expression.CreateMultiValued(values, new Division());
        
        // Act
        var result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
    
    [Test]
    [TestCase(12, 4, 8)]
    public void Subtraction(double minuend, double subtrahend, double expectedResult)
    {
        // Arrange
        var values = new List<double> { minuend, subtrahend };
        var expression = Expression.CreateMultiValued(values, new Subtraction());
        
        // Act
        var result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
}