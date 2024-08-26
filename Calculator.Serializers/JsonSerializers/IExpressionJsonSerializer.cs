using Calculator.Core;

namespace Calculator.Serializers.JsonSerializers;

public interface IExpressionJsonSerializer
{
    public string Serialize(Expression expression);
    public Expression? Deserialize(string json);
}