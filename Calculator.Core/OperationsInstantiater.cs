using System.Reflection;

namespace Calculator.Core;

public sealed class OperationsInstantiater
{
    private static readonly Dictionary<string, Type> OperationTypes;
    static OperationsInstantiater()
    {
        OperationTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(Operation).IsAssignableFrom(t) && !t.IsAbstract)
            .Where(t => t.GetConstructor(Type.EmptyTypes) != null)
            .ToDictionary(t => ((Operation)Activator.CreateInstance(t)!).Name, t => t);
    }
    public Operation Create(string operationName)
    {
        if (OperationTypes.TryGetValue(operationName, out Type? operationType))
        {
            return (Operation)Activator.CreateInstance(operationType)!;
        }

        throw new Exception($"Unsupported operation name: {operationName}");
    }
}