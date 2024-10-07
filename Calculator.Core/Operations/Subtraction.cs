namespace Calculator.Core.Operations;

public sealed class Subtraction : Operation
{
    public override string Name => "Subtraction";
    protected override Arity Arity => Arity.CreateFixed(2);
    public override double Apply(List<double> values)
    {
        return values[0] - values[1];
    }
}