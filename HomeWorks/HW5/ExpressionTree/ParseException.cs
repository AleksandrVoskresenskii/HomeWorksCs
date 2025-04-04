namespace ExpressionTree;

/// <summary>
/// Представляет ошибки, возникающие при разборе арифметического выражения.
/// </summary>
public class ParseException : Exception
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ParseException"/> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    public ParseException(string message)
        : base(message)
    {
    }
}
