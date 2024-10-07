namespace Calculator.Core.Operations;

public sealed class Addition : Operation
{
    public override string Name => OperationName;
    public const string OperationName = "Addition";
    protected override Arity Arity => Arity.CreateVarying();
    public override double Apply(List<double> values)
    {
        return values.Sum();
    }
}