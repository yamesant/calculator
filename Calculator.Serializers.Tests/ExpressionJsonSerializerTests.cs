using Calculator.Serializers.JsonSerializers;

namespace Calculator.Serializers.Tests;

public sealed class ExpressionJsonSerializerTests
{
    private readonly ISerializer _sut = new ExpressionJsonSerializer();

    [Test]
    public void CanSerializeSingleValued()
    {
        // Arrange
        Expression expression = Expression.CreateSingleValued(1);
        string expected =
            """
            {"Value": 1}
            """.Replace("\r", "").Replace("\n", "").Replace(" ", "");
        
        // Act
        string result = _sut.Serialize(expression);
        
        // Assert
        result.Should().Be(expected);
    }
    [Test]
    public void CanSerializeMultiValued()
    {
        // Arrange
        Expression expression = Expression.CreateMultiValued([3, 3], new Addition());
        string expected =
            """
            {
            "Operation": "Addition",
            "Subexpressions":[
                {"Value":3},
                {"Value":3}
            ]}
            """.Replace("\r", "").Replace("\n", "").Replace(" ", "");
        
        // Act
        string result = _sut.Serialize(expression);
        
        // Assert
        result.Should().Be(expected);
    }
    
    [Test]
    public void CanSerializeNested()
    {
        // Arrange
        Expression expression = Expression.CreateNested(
        [
            Expression.CreateSingleValued(2),
            Expression.CreateMultiValued([3, 1], new Subtraction()),
            Expression.CreateSingleValued(2)
        ], new Multiplication());
        
        string expected =
            """
            {
                "Operation": "Multiplication",
                "Subexpressions": [
                    { "Value": 2 },
                    {
                        "Operation": "Subtraction",
                        "Subexpressions": [
                            { "Value": 3 },
                            { "Value": 1 }
                        ]
                    },
                    { "Value": 2 }
                ]
            }
            """.Replace("\r", "").Replace("\n", "").Replace(" ", "");
        
        // Act
        string result = _sut.Serialize(expression);
        
        // Assert
        result.Should().Be(expected);
    }
    
    [Test]
    public void CanDeserializeSingleValued()
    {
        // Arrange
        string data =
            """
            {"Value": 1}
            """.Replace("\r", "").Replace("\n", "").Replace(" ", "");
        Expression expected = Expression.CreateSingleValued(1);
        
        // Act
        Expression? result = _sut.Deserialize(data);
        
        // Assert
        result.Should().Be(expected);
    }
    
    [Test]
    public void CanDeserializeMultiValued()
    {
        // Arrange
        string data =
            """
            {
            "Operation": "Addition",
            "Subexpressions": [
                {"Value": 3},
                {"Value": 3}
            ]}
            """.Replace("\r", "").Replace("\n", "").Replace(" ", "");
        Expression expected = Expression.CreateMultiValued([3, 3], new Addition());
        
        // Act
        Expression? result = _sut.Deserialize(data);
        
        // Assert
        result.Should().Be(expected);
    }
    
    [Test]
    public void CanDeserializeNested()
    {
        // Arrange
        string data =
            """
            {
                "Operation": "Multiplication",
                "Subexpressions": [
                    { "Value": 2 },
                    {
                        "Operation": "Subtraction",
                        "Subexpressions": [
                            { "Value": 3 },
                            { "Value": 1 }
                        ]
                    },
                    { "Value": 2 }
                ]
            }
            """.Replace("\r", "").Replace("\n", "").Replace("  ", "");
        
        Expression expected = Expression.CreateNested(
        [
            Expression.CreateSingleValued(2),
            Expression.CreateMultiValued([3, 1], new Subtraction()),
            Expression.CreateSingleValued(2)
        ], new Multiplication());
        
        // Act
        Expression? result = _sut.Deserialize(data);
        
        // Assert
        result.Should().Be(expected);
    }
}