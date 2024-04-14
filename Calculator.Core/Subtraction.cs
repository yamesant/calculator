namespace Calculator.Core;

public sealed class Subtraction : Operation
{
    public override double Apply(List<double> values)
    {
        return values[0] - values[1];
    }
}