namespace Calculator.Core.Operations;

public sealed class StandardDeviation : Operation
{
    public override string Name => "StdDev";
    protected override Arity Arity => Arity.CreateVarying();
    public override double Apply(List<double> values)
    {
        int n = values.Count;
        if (n == 0) return 0;
        double mu = values.Sum() / n;
        return Math.Sqrt(values.Select(x => Math.Pow(x - mu, 2)).Sum() / n);
    }
}