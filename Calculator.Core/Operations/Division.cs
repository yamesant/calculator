namespace Calculator.Core.Operations;

public sealed class Division : Operation
{
    public override string Name => "Division";
    protected override Arity Arity => Arity.CreateFixed(2);
    public override double Apply(List<double> values)
    {
        return values[0] / values[1];
    }
}