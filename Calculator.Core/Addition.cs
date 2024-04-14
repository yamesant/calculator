namespace Calculator.Core;

public sealed class Addition : Operation
{
    public override double Apply(List<double> values)
    {
        return values.Sum();
    }
}