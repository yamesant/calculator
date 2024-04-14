namespace Calculator.Core;

public sealed class Constant(double value) : Operation
{
    protected override Arity Arity => Arity.CreateFixed(0);

    public override double Apply(List<double> values)
    {
        return value;
    }
}