using FluentAssertions;

namespace Calculator.Core.Tests;

public sealed class ExpressionJsonSerializerTests
{
    private readonly IExpressionJsonSerializer _sut = new ExpressionJsonSerializer();

    [Test]
    public void CanSerializeSingleValued()
    {
        // Arrange
        Expression expression = Expression.CreateSingleValued(1);
        string expectedJson = @"{""Value"":1}";
        
        // Act
        string json = _sut.Serialize(expression);
        
        // Assert
        json.Should().Be(expectedJson);
    }
    [Test]
    public void CanSerializeMultiValued()
    {
        // Arrange
        Expression expression = Expression.CreateMultiValued([3, 3], new Addition());
        string expectedJson = @"{""Operation"":""Addition"",""Subexpressions"":[{""Value"":3},{""Value"":3}]}";
        
        // Act
        string json = _sut.Serialize(expression);
        
        // Assert
        json.Should().Be(expectedJson);
    }
    
    [Test]
    public void CanDeserializeSingleValued()
    {
        // Arrange
        string json = @"{""Value"": 1}";
        Expression expectedExpression = Expression.CreateSingleValued(1);
        
        // Act
        Expression? expression = _sut.Deserialize(json);
        
        // Assert
        expression.Should().Be(expectedExpression);
    }
    
    [Test]
    public void CanDeserializeMultiValued()
    {
        // Arrange
        string json = @"{
            ""Operation"": ""Addition"",
            ""Subexpressions"": [{""Value"": 3}, {""Value"": 3}]
            }";
        Expression expectedExpression = Expression.CreateMultiValued([3, 3], new Addition());
        
        // Act
        Expression? expression = _sut.Deserialize(json);
        
        // Assert
        expression.Should().Be(expectedExpression);
    }
}