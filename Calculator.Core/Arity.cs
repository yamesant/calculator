namespace Calculator.Core;

public sealed class Arity
{
    public bool IsAny { get; }
    public int Value { get; }

    private Arity(bool isAny, int value)
    {
        IsAny = isAny;
        Value = value;
    }

    public static Arity CreateVarying() => new Arity(true, -1);
    public static Arity CreateFixed(int n) => new Arity(false, n);
}