namespace Calculator.Core;

public abstract class Operation : ValueObject
{
    public abstract string Name { get; }
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
            Addition.OperationName => new Addition(),
            Division.OperationName => new Division(),
            Multiplication.OperationName => new Multiplication(),
            Subtraction.OperationName => new Subtraction(),
            _ => throw new Exception("Unsupported operation name")
        };
    }
}