/// <summary>
/// Представляет бинарную операцию в арифметическом выражении.
/// </summary>
namespace ExpressionTree;

public class BinaryOperationExpression : Expression
{
    /// <summary>
    /// Получает символ оператора ('+', '-', '*', '/').
    /// </summary>
    public char Operator { get; }

    /// <summary>
    /// Получает левый операнд.
    /// </summary>
    public Expression Left { get; }

    /// <summary>
    /// Получает правый операнд.
    /// </summary>
    public Expression Right { get; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="BinaryOperationExpression"/>.
    /// </summary>
    /// <param name="op">Символ оператора.</param>
    /// <param name="left">Левый операнд.</param>
    /// <param name="right">Правый операнд.</param>
    public BinaryOperationExpression(char op, Expression left, Expression right)
    {
        Operator = op;
        Left = left;
        Right = right;
    }

    /// <inheritdoc />
    public override int Evaluate()
    {
        int leftValue = Left.Evaluate();
        int rightValue = Right.Evaluate();

        return Operator switch
        {
            '+' => leftValue + rightValue,
            '-' => leftValue - rightValue,
            '*' => leftValue * rightValue,
            '/' => rightValue != 0 ? leftValue / rightValue : throw new DivideByZeroException("Ошибка: деление на ноль."),
            _ => throw new InvalidOperationException($"Ошибка: неизвестный оператор '{Operator}'.")
        };
    }

    /// <inheritdoc />
    public override string Print() => $"( {Operator} {Left.Print()} {Right.Print()} )";
}
