namespace Calculator.Core;

public sealed class ExpressionJsonSerializer : IExpressionJsonSerializer
{
    public string Serialize(Expression expression)
    {
        throw new NotImplementedException();
    }

    public Expression? Deserialize(string json)
    {
        throw new NotImplementedException();
    }

    private sealed class ExpressionModel
    {
        public double? Value { get; set; }
        public string? Operation { get; set; }
        public List<ExpressionModel>? Subexpressions { get; set; }
    }
}