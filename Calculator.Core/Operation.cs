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
    
    public static Operation FromName(string operationName)
    {
        return operationName switch
        {
            Addition.Name => new Addition(),
            Division.Name => new Division(),
            Multiplication.Name => new Multiplication(),
            Subtraction.Name => new Subtraction(),
            _ => throw new Exception("Unsupported operation name")
        };
    }
}