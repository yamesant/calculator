﻿namespace Calculator.Core;

public sealed class Expression
{
    private readonly List<double> _values;
    private readonly Operation _operation;
    private Expression(List<double> values, Operation operation)
    {
        _values = values;
        _operation = operation;
    }
    public static Expression CreateSingleValued(double value)
    {
        return new Expression(new List<double>(), new Constant(value));
    }

    public double Evaluate()
    {
        return _operation.Apply(_values);
    }
}