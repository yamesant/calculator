namespace Calculator.Core.Operations;

public sealed class Direction : Operation
{
    public override string Name => "Direction";
    protected override Arity Arity => Arity.CreateFixed(2);
    public override double Apply(List<double> values)
    {
        return Math.Sign(values[1] - values[0]);
    }
}