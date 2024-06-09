namespace Calculator.Core;

public sealed class Expression : ValueObject
{
    private readonly List<Expression> _subexpressions;
    private readonly Operation _operation;
    private Expression(List<Expression> subexpressions, Operation operation)
    {
        if (!operation.CanApply(subexpressions.Count))
            throw new ArgumentException("Operation arity does not match the number of arguments");
        _subexpressions = subexpressions;
        _operation = operation;
    }
    public static Expression CreateSingleValued(double value)
    {
        return new Expression(new List<Expression>(), new Constant(value));
    }

    public static Expression CreateMultiValued(List<double> values, Operation operation)
    {
        var subexpressions = values.Select(CreateSingleValued).ToList();
        return new Expression(subexpressions, operation);
    }
    
    public static Expression CreateNested(List<Expression> subexpressions, Operation operation)
    {
        return new Expression(subexpressions, operation);
    }

    public double Evaluate()
    {
        var values = _subexpressions
            .Select(subexpression => subexpression.Evaluate())
            .ToList();
        return _operation.Apply(values);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _operation;
        foreach (Expression subexpression in _subexpressions)
        {
            yield return subexpression;
        }
    }
}