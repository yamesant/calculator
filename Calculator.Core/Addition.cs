namespace Calculator.Core;

public sealed class Addition : Operation
{
    public const string Name = "Addition";
    protected override Arity Arity => Arity.CreateVarying();
    public override double Apply(List<double> values)
    {
        return values.Sum();
    }
}