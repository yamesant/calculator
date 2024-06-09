namespace Calculator.Core;

public abstract class Operation : ValueObject
{
    protected abstract Arity Arity { get; }
    public abstract double Apply(List<double> values);

    public bool CanApply(int numberOfArguments)
    {
        if (Arity.IsAny) return true;
        return Arity.Value == numberOfArguments;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield break;
    }
}