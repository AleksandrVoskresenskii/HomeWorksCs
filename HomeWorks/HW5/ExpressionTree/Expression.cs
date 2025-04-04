/// <summary>
/// Представляет арифметическое выражение.
/// </summary>
namespace ExpressionTree;

public abstract class Expression
{
    /// <summary>
    /// Вычисляет значение выражения.
    /// </summary>
    /// <returns>Целочисленное значение выражения.</returns>
    public abstract int Evaluate();

    /// <summary>
    /// Возвращает строковое представление выражения.
    /// </summary>
    /// <returns>Строковое представление выражения.</returns>
    public abstract string Print();
}
