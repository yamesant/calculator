namespace Calculator.Core;

public sealed class Constant(double value) : Operation
{
    public override string Name => $"Constant({value})";
    protected override Arity Arity => Arity.CreateFixed(0);

    public override double Apply(List<double> values)
    {
        return value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return value;
        foreach (var baseEqualityComponent in base.GetEqualityComponents())
        {
            yield return baseEqualityComponent;
        }
    }
}