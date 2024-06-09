using FluentAssertions;

namespace Calculator.Core.Tests;

public sealed class OperationTests
{
    [Test]
    public void SameOperationsEqual()
    {
        // Arrange
        Operation p1 = new Addition();
        Operation p2 = new Addition();
        
        // Assert
        p1.Should().Be(p2);
    }

    [Test]
    public void DifferentOperationsNotEqual()
    {
        // Arrange
        Operation p1 = new Addition();
        Operation p2 = new Subtraction();
        
        // Assert
        p1.Should().NotBe(p2);
    }
}