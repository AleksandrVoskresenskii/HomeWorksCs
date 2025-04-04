namespace ExpressionTree;

/// <summary>
/// Предоставляет функциональность для вычисления значения арифметического выражения.
/// </summary>
public class ExpressionCalculator
{
    /// <summary>
    /// Вычисляет значение заданного арифметического выражения.
    /// </summary>
    /// <param name="expr">Арифметическое выражение.</param>
    /// <returns>Результат вычисления выражения.</returns>
    public int Calculate(Expression expr) => expr.Evaluate();
}
