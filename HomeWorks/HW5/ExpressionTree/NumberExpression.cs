/// <summary>
/// Представляет числовой операнд арифметического выражения.
/// </summary>
namespace ExpressionTree;

public class NumberExpression : Expression
{
    /// <summary>
    /// Получает числовое значение операнда.
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="NumberExpression"/>.
    /// </summary>
    /// <param name="value">Числовое значение.</param>
    public NumberExpression(int value)
    {
        Value = value;
    }

    /// <inheritdoc />
    public override int Evaluate() => Value;

    /// <inheritdoc />
    public override string Print() => Value.ToString();
}
