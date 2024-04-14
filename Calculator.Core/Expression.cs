namespace Calculator.Core;

public sealed class Expression
{
    private readonly List<double> _values;
    private readonly Operation _operation;
    private Expression(List<double> values, Operation operation)
    {
        if (!operation.CanApply(values.Count))
            throw new ArgumentException("Operation arity does not match the number of arguments");
        _values = values;
        _operation = operation;
    }
    public static Expression CreateSingleValued(double value)
    {
        return new Expression(new List<double>(), new Constant(value));
    }

    public static Expression CreateMultiValued(List<double> values, Operation operation)
    {
        return new Expression(values, operation);
    }

    public double Evaluate()
    {
        return _operation.Apply(_values);
    }
}