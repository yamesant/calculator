using Calculator.Core;

namespace Calculator.Serializers;

public interface ISerializer
{
    string Serialize(Expression expression);
    Expression? Deserialize(string data);
}