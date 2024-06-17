namespace Calculator.Core;

public interface IExpressionJsonSerializer
{
    public string Serialize(Expression expression);
    public Expression? Deserialize(string json);
}