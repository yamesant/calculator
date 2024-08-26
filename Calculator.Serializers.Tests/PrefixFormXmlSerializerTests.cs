using Calculator.Serializers.PrefixFormXmlSerializers;

namespace Calculator.Serializers.Tests;

public sealed class PrefixFormXmlSerializerTests
{
    private readonly ISerializer _sut = new PrefixFormXmlSerializer();
    
    [Test]
    public void CanSerializeSingleValued()
    {
        // Arrange
        Expression expression = Expression.CreateSingleValued(-1);
        string expected =
            """
            <?xml version="1.0" encoding="utf-16"?>
            <Data xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                <Elements>
                    <Element xsi:type="Value">-1</Element>
                </Elements>
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
                <Elements>
                    <Element xsi:type="Operation">
                        <Name>Addition</Name>
                        <Arity>2</Arity>
                    </Element>
                    <Element xsi:type="Value">1.5</Element>
                    <Element xsi:type="Value">1.5</Element>
                </Elements>
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
                <Elements>
                    <Element xsi:type="Operation">
                        <Name>Multiplication</Name>
                        <Arity>3</Arity>
                    </Element>
                    <Element xsi:type="Value">2</Element>
                    <Element xsi:type="Operation">
                        <Name>Subtraction</Name>
                        <Arity>2</Arity>
                    </Element>
                    <Element xsi:type="Value">3</Element>
                    <Element xsi:type="Value">1</Element>
                    <Element xsi:type="Value">2</Element>
                </Elements>
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
                <Elements>
                    <Element xsi:type="Value">-1</Element>
                </Elements>
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
                <Elements>
                    <Element xsi:type="Operation">
                        <Name>Addition</Name>
                        <Arity>2</Arity>
                    </Element>
                    <Element xsi:type="Value">1.5</Element>
                    <Element xsi:type="Value">1.5</Element>
                </Elements>
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
                <Elements>
                    <Element xsi:type="Operation">
                        <Name>Multiplication</Name>
                        <Arity>3</Arity>
                    </Element>
                    <Element xsi:type="Value">2</Element>
                    <Element xsi:type="Operation">
                        <Name>Subtraction</Name>
                        <Arity>2</Arity>
                    </Element>
                    <Element xsi:type="Value">3</Element>
                    <Element xsi:type="Value">1</Element>
                    <Element xsi:type="Value">2</Element>
                </Elements>
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
    
    [Test]
    public void FailToDeserializeWhenNotEnoughElements()
    {
        // Arrange
        string data =
            """
                <?xml version="1.0" encoding="utf-16"?>
                <Data xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                    <Elements>
                        <Element xsi:type="Operation">
                            <Name>Addition</Name>
                            <Arity>2</Arity>
                        </Element>
                        <Element xsi:type="Value">1</Element>
                    </Elements>
                </Data>
                """.Replace("\r", "").Replace("\n", "").Replace("  ", "");
        
        // Act
        Expression? result = _sut.Deserialize(data);
        
        // Assert
        result.Should().BeNull();
    }
    
    [Test]
    public void FailToDeserializeWhenTooManyElements()
    {
        // Arrange
        string data =
            """
                <?xml version="1.0" encoding="utf-16"?>
                <Data xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                    <Elements>
                        <Element xsi:type="Operation">
                            <Name>Addition</Name>
                            <Arity>2</Arity>
                        </Element>
                        <Element xsi:type="Value">1</Element>
                        <Element xsi:type="Value">1</Element>
                        <Element xsi:type="Value">1</Element>
                    </Elements>
                </Data>
                """.Replace("\r", "").Replace("\n", "").Replace("  ", "");
        
        // Act
        Expression? result = _sut.Deserialize(data);
        
        // Assert
        result.Should().BeNull();
    }
}