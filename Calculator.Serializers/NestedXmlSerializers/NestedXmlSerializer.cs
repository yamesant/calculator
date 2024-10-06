using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Calculator.Core;

namespace Calculator.Serializers.NestedXmlSerializers;

public sealed class NestedXmlSerializer(OperationsInstantiater operationsInstantiater) : ISerializer
{
    public string Serialize(Expression expression)
    {
        Data data = new()
        {
            Expression = ConvertToModel(expression)
        };
        XmlSerializer serializer = new(typeof(Data));
        XmlWriterSettings settings = new() { Encoding = Encoding.Unicode, OmitXmlDeclaration = false };
        using StringWriter stringWriter = new();
        using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings);
        serializer.Serialize(xmlWriter, data);
        return stringWriter.ToString();
    }
    
    private ExpressionModel ConvertToModel(Expression expression)
    {
        if (expression.Subexpressions.Count == 0)
        {
            return new ValueExpressionModel
            {
                Value = expression.Evaluate()
            };
        }
        return new OperationExpressionModel
        {
            Name = expression.Operation.Name,
            Subexpressions = expression.Subexpressions.Select(ConvertToModel).ToList()
        };
    }

    public Expression? Deserialize(string data)
    {
        XmlSerializer serializer = new(typeof(Data));
        using StringReader stringReader = new(data);
        Data? dataModel =  (Data?)serializer.Deserialize(stringReader);
        return dataModel is null ? null : ConvertFromModel(dataModel.Expression);
    }

    private Expression? ConvertFromModel(ExpressionModel model)
    {
        if (model is ValueExpressionModel valueExpressionModel)
        {
            return Expression.CreateSingleValued(valueExpressionModel.Value);
        }

        if (model is OperationExpressionModel operationExpressionModel)
        {
            List<Expression> subexpressions = [];
            foreach (ExpressionModel submodel in operationExpressionModel.Subexpressions)
            {
                Expression? subexpression = ConvertFromModel(submodel);
                if (subexpression is null)
                {
                    return null;
                }

                subexpressions.Add(subexpression);
            }

            return Expression.CreateNested(
                subexpressions,
                operationsInstantiater.Create(operationExpressionModel.Name)
            );
        }

        return null;
    }
}