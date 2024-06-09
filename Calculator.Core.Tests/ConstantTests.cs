using FluentAssertions;

namespace Calculator.Core.Tests;

public sealed class ConstantTests
{
    [Test]
    public void SameConstantsEqual()
    {
        // Arrange
        Constant c1 = new Constant(1);
        Constant c2 = new Constant(1);
        
        // Assert
        c1.Should().Be(c2);
    }

    [Test]
    public void DifferentConstantsNotEqual()
    {
        // Arrange
        Constant c1 = new Constant(1);
        Constant c2 = new Constant(2);
        
        // Assert
        c1.Should().NotBe(c2);
    }
}