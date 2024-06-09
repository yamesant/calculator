using System.Text.Json;

namespace Calculator.Core;

public sealed class ExpressionJsonSerializer : IExpressionJsonSerializer
{
    public string Serialize(Expression expression)
    {
        throw new NotImplementedException();
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
        public double? Value { get; set; }
        public string? Operation { get; set; }
        public List<ExpressionModel>? Subexpressions { get; set; }
    }
}