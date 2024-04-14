using AutoFixture.NUnit3;
using FluentAssertions;

namespace Calculator.Core.Tests;

public sealed class Tests
{
    [Test, AutoData]
    public void SingleValue(double value)
    {
        // Arrange
        var expression = Expression.CreateSingleValued(value);
        
        // Act
        var result = expression.Evaluate();

        // Assert
        result.Should().Be(value);
    }
    
    [Test, AutoData]
    public void SingleValue(double firstSummand, double secondSummand)
    {
        // Arrange
        var values = new List<double> { firstSummand, secondSummand };
        var expression = Expression.CreateMultiValued(values, new Addition());
        var expectedResult = firstSummand + secondSummand;
        
        // Act
        var result = expression.Evaluate();

        // Assert
        result.Should().Be(expectedResult);
    }
}