namespace Calculator.Core.Operations;

public sealed class Logarithm : Operation
{
    public override string Name => "Logarithm";
    protected override Arity Arity => Arity.CreateFixed(1);
    public override double Apply(List<double> values)
    {
        return Math.Log(values[0]);
    }
}