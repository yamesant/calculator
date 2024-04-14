using AutoFixture.NUnit3;
using FluentAssertions;

namespace Calculator.Core.Tests;

public class Tests
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
}