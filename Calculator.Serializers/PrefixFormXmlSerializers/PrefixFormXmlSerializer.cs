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
        XmlSerializer serializer = new(typeof(Data));
        using StringReader stringReader = new(data);
        Data? dataModel =  (Data?)serializer.Deserialize(stringReader);
        if (dataModel is null)
        {
            return null;
        }
        List<ExpressionElement> elements = dataModel.Elements;
        int next = 0;
        Expression? expression = Process();
        return expression;

        Expression? Process()
        {
            if (next == elements.Count)
            {
                return null;
            }
            
            if (elements[next] is ValueElement valueElement)
            {
                next++;
                return Expression.CreateSingleValued(valueElement.Value);
            }
            
            if (elements[next] is OperationElement operationElement)
            {
                next++;
                Operation operation = Operation.FromName(operationElement.Name);
                List<Expression> subexpressions = new();
                for (int i = 0; i < operationElement.Arity; i++)
                {
                    Expression? subexpression = Process();
                    if (subexpression is null)
                    {
                        return null;
                    }
                    subexpressions.Add(subexpression);
                }

                return Expression.CreateNested(subexpressions, operation);
            }

            return null;
        }
    }
}