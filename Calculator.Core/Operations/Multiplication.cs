namespace Calculator.Core.Operations;

public sealed class Multiplication : Operation
{
    public override string Name => "Multiplication";
    protected override Arity Arity => Arity.CreateVarying();
    public override double Apply(List<double> values)
    {
        var product = 1.0;
        foreach (var value in values)
        {
            product *= value;
        }

        return product;
    }
}