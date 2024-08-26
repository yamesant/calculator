using System.Xml.Serialization;

namespace Calculator.Serializers.PrefixFormXmlSerializers;

[XmlType(TypeName="Data")]
public sealed class Data
{
    public List<ExpressionElement> Elements { get; set; }
}

[XmlInclude(typeof(OperationElement))]
[XmlInclude(typeof(ValueElement))]
[XmlType(TypeName="Element")]
public abstract class ExpressionElement
{
}

[XmlType(TypeName = "Operation")]
public sealed class OperationElement : ExpressionElement
{
    public string Name { get; set; }
    public int Arity { get; set; }
}

[XmlType(TypeName = "Value")]
public sealed class ValueElement : ExpressionElement
{
    [XmlText]
    public double Value { get; set; }
}