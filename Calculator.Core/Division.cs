namespace Calculator.Core;

public sealed class Division : Operation
{
    public const string Name = "Division";
    protected override Arity Arity => Arity.CreateFixed(2);
    public override double Apply(List<double> values)
    {
        return values[0] / values[1];
    }
}