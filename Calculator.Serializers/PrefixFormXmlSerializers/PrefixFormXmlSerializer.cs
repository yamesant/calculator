using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Calculator.Core;

namespace Calculator.Serializers.PrefixFormXmlSerializers;

public sealed class PrefixFormXmlSerializer : ISerializer
{
    public string Serialize(Expression expression)
    {
        Data data = ConvertToModel(expression);
        XmlSerializer serializer = new(typeof(Data));
        XmlWriterSettings settings = new() { Encoding = Encoding.Unicode, OmitXmlDeclaration = false };
        using StringWriter stringWriter = new();
        using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings);
        serializer.Serialize(xmlWriter, data);
        return stringWriter.ToString();
    }

    private Data ConvertToModel(Expression expression)
    {
        List<ExpressionElement> elements = new();
        Data data = new() { Elements = elements };
        Process(expression);
        return data;

        void Process(Expression expression)
        {
            if (expression.Subexpressions.Count == 0)
            {
                ValueElement element = new() { Value = expression.Evaluate() };
                elements.Add(element);
            }
            else
            {
                OperationElement element = new()
                {
                    Name = expression.Operation.Name,
                    Arity = expression.Subexpressions.Count,
                };
                elements.Add(element);
                foreach (Expression subexpression in expression.Subexpressions)
                {
                    Process(subexpression);
                }
            }
        }
    }

    public Expression? Deserialize(string data)
    {
        throw new NotImplementedException();
    }
}