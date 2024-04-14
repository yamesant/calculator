namespace Calculator.Core;

public sealed class Division : Operation
{
    public override double Apply(List<double> values)
    {
        return values[0] / values[1];
    }
}