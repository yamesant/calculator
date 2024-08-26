using System.Xml.Serialization;

namespace Calculator.Serializers.NestedXmlSerializers;

[XmlType(TypeName="Data")]
public sealed class Data
{
    public ExpressionModel Expression { get; set; }
}

[XmlInclude(typeof(OperationExpressionModel))]
[XmlInclude(typeof(ValueExpressionModel))]
[XmlType(TypeName="Expression")]
public abstract class ExpressionModel
{
}

[XmlType(TypeName = "Operation")]
public sealed class OperationExpressionModel : ExpressionModel
{
    [XmlAttribute]
    public string Name { get; set; }
    public List<ExpressionModel> Subexpressions { get; set; }
}

[XmlType(TypeName = "Value")]
public sealed class ValueExpressionModel : ExpressionModel
{
    [XmlText]
    public double Value { get; set; }
}