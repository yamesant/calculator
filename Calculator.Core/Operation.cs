namespace Calculator.Core;

public abstract class Operation
{
    protected abstract Arity Arity { get; }
    public abstract double Apply(List<double> values);

    public bool CanApply(int numberOfArguments)
    {
        if (Arity.IsAny) return true;
        return Arity.Value == numberOfArguments;
    }
}