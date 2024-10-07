namespace Calculator.Core.Operations;

public sealed class Exponentiation : Operation
{
    public override string Name => "Exponentiation";
    protected override Arity Arity => Arity.CreateFixed(1);
    public override double Apply(List<double> values)
    {
        return Math.Exp(values[0]);
    }
}