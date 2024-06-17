namespace Calculator.Core;

public sealed class Multiplication : Operation
{
    public override string Name => OperationName;
    public const string OperationName = "Multiplication";
    protected override Arity Arity => Arity.CreateVarying();
    public override double Apply(List<double> values)
    {
        var product = 1.0;
        foreach (var value in values)
        {
            product *= value;
        }

        return product;
    }
}