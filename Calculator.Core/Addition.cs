namespace Calculator.Core;

public sealed class Addition : Operation
{
    protected override Arity Arity => Arity.CreateVarying();
    public override double Apply(List<double> values)
    {
        return values.Sum();
    }
}