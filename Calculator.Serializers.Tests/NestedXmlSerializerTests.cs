using Calculator.Serializers.NestedXmlSerializers;

namespace Calculator.Serializers.Tests;

public sealed class NestedXmlSerializerTests
{
    private readonly ISerializer _sut = new NestedXmlSerializer();
    
    [Test]
    public void CanSerializeSingleValued()
    {
        // Arrange
        Expression expression = Expression.CreateSingleValued(-1);
        string expected =
            """
            <?xml version="1.0" encoding="utf-16"?>
            <Data xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                <Expression xsi:type="Value">-1</Expression>
            </Data>
            """.Replace("\r", "").Replace("\n", "").Replace("  ", "");
        
        // Act
        string result = _sut.Serialize(expression);
        
        // Assert
        result.Should().Be(expected);
    }
    
    [Test]
    public void CanSerializeMultiValued()
    {
        // Arrange
        Expression expression = Expression.CreateMultiValued([1.5, 1.5], new Addition());
        string expected =
            """
            <?xml version="1.0" encoding="utf-16"?>
            <Data xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                <Expression xsi:type="Operation" Name="Addition">
                    <Subexpressions>
                        <Expression xsi:type="Value">1.5</Expression>
                        <Expression xsi:type="Value">1.5</Expression>
                    </Subexpressions>
                </Expression>
            </Data>
            """.Replace("\r", "").Replace("\n", "").Replace("  ", "");
        
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
            <?xml version="1.0" encoding="utf-16"?>
            <Data xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                <Expression xsi:type="Operation" Name="Multiplication">
                    <Subexpressions>
                        <Expression xsi:type="Value">2</Expression>
                        <Expression xsi:type="Operation" Name="Subtraction">
                            <Subexpressions>
                                <Expression xsi:type="Value">3</Expression>
                                <Expression xsi:type="Value">1</Expression>
                            </Subexpressions>
                        </Expression>
                        <Expression xsi:type="Value">2</Expression>
                    </Subexpressions>
                </Expression>
            </Data>
            """.Replace("\r", "").Replace("\n", "").Replace("  ", "");
        
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
            <?xml version="1.0" encoding="utf-16"?>
            <Data xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                <Expression xsi:type="Value">-1</Expression>
            </Data>
            """.Replace("\r", "").Replace("\n", "").Replace("  ", "");
        
        Expression expected = Expression.CreateSingleValued(-1);
        
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
            <?xml version="1.0" encoding="utf-16"?>
            <Data xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                <Expression xsi:type="Operation" Name="Addition">
                    <Subexpressions>
                        <Expression xsi:type="Value">1.5</Expression>
                        <Expression xsi:type="Value">1.5</Expression>
                    </Subexpressions>
                </Expression>
            </Data>
            """.Replace("\r", "").Replace("\n", "").Replace("  ", "");
        Expression expected = Expression.CreateMultiValued([1.5, 1.5], new Addition());
        
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
            <?xml version="1.0" encoding="utf-16"?>
            <Data xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                <Expression xsi:type="Operation" Name="Multiplication">
                    <Subexpressions>
                        <Expression xsi:type="Value">2</Expression>
                        <Expression xsi:type="Operation" Name="Subtraction">
                            <Subexpressions>
                                <Expression xsi:type="Value">3</Expression>
                                <Expression xsi:type="Value">1</Expression>
                            </Subexpressions>
                        </Expression>
                        <Expression xsi:type="Value">2</Expression>
                    </Subexpressions>
                </Expression>
            </Data>
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