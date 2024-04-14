namespace Calculator.Core;

public sealed class Constant(double value) : Operation
{
    public override double Apply(List<double> values)
    {
        return value;
    }
}