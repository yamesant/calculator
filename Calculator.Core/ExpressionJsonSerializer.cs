using System.Text.Json;
using System.Text.Json.Serialization;

namespace Calculator.Core;

public sealed class ExpressionJsonSerializer : IExpressionJsonSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    public string Serialize(Expression expression)
    {
        ExpressionModel model = ConvertToModel(expression);
        return JsonSerializer.Serialize(model, Options);
    }

    private ExpressionModel ConvertToModel(Expression expression)
    {
        if (expression.Subexpressions.Count == 0)
        {
            return new ExpressionModel { Value = expression.Evaluate() };
        }

        ExpressionModel result = new()
        {
            Operation = expression.Operation.Name,
            Subexpressions = expression.Subexpressions.Select(ConvertToModel).ToList(),
        };
        return result;
    }

    public Expression? Deserialize(string json)
    {
        var expressionModel = JsonSerializer.Deserialize<ExpressionModel>(json);
        if (expressionModel is null)
        {
            return null;
        }

        return ConvertFromModel(expressionModel);
    }

    private Expression? ConvertFromModel(ExpressionModel expressionModel)
    {
        if (expressionModel.Value is not null)
        {
            if (expressionModel.Operation is not null || expressionModel.Subexpressions is not null)
            {
                return null;
            }

            return Expression.CreateSingleValued(expressionModel.Value.Value);
        }

        if (expressionModel.Operation is null || expressionModel.Subexpressions is null)
        {
            return null;
        }

        Operation operation = Operation.FromName(expressionModel.Operation);
        List<Expression> subexpressions = new();
        foreach (ExpressionModel subexpressionDto in expressionModel.Subexpressions)
        {
            Expression? subexpression = ConvertFromModel(subexpressionDto);
            if (subexpression is null)
            {
                return null;
            }

            subexpressions.Add(subexpression);
        }

        return Expression.CreateNested(subexpressions, operation);
    }

    private sealed class ExpressionModel
    {
        public double? Value { get; init; }
        public string? Operation { get; init; }
        public List<ExpressionModel>? Subexpressions { get; init; }
    }
}