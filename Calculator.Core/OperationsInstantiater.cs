namespace Calculator.Core;

public sealed class OperationsInstantiater
{
    public Operation Create(string operationName)
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