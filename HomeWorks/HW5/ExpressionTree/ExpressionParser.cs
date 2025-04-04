using System;
using System.IO;
using System.Text;

namespace ExpressionTree;

/// <summary>
/// Предоставляет функциональность для разбора арифметического выражения из строки.
/// </summary>
public class ExpressionParser
{
    private readonly StringReader reader;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ExpressionParser"/>.
    /// </summary>
    /// <param name="input">Строка, содержащая арифметическое выражение.</param>
    public ExpressionParser(string input)
    {
        reader = new StringReader(input);
    }

    /// <summary>
    /// Разбирает арифметическое выражение.
    /// </summary>
    /// <returns>Дерево разбора выражения.</returns>
    /// <exception cref="ParseException">Выбрасывается при ошибках разбора.</exception>
    public Expression ParseExpression()
    {
        SkipWhiteSpace();
        int next = reader.Peek();
        if (next == -1)
        {
            throw new ParseException("Неожиданный конец входных данных.");
        }

        char ch = (char)next;
        if (ch == '(')
        {
            // Потребляем открывающую скобку
            reader.Read();
            SkipWhiteSpace();

            int opInt = reader.Read();
            if (opInt == -1)
            {
                throw new ParseException("Неожиданный конец входных данных при ожидании оператора.");
            }
            char op = (char)opInt;
            if (op != '+' && op != '-' && op != '*' && op != '/')
            {
                throw new ParseException($"Некорректный оператор '{op}'.");
            }

            Expression left = ParseExpression();
            Expression right = ParseExpression();

            SkipWhiteSpace();
            int closing = reader.Read();
            if (closing != ')')
            {
                throw new ParseException("Ожидалась закрывающая скобка ')'.");
            }
            return new BinaryOperationExpression(op, left, right);
        }
        else
        {
            return ParseNumber();
        }
    }

    /// <summary>
    /// Разбирает число (целое) из входных данных.
    /// </summary>
    /// <returns>Объект <see cref="NumberExpression"/> с разобранным числом.</returns>
    /// <exception cref="ParseException">Выбрасывается при ошибках разбора числа.</exception>
    private NumberExpression ParseNumber()
    {
        SkipWhiteSpace();
        StringBuilder sb = new StringBuilder();
        int next = reader.Peek();
        if (next == -1)
        {
            throw new ParseException("Неожиданный конец входных данных при ожидании числа.");
        }

        char ch = (char)next;
        if (ch == '-' || char.IsDigit(ch))
        {
            sb.Append((char)reader.Read());
            while (true)
            {
                next = reader.Peek();
                if (next == -1)
                {
                    break;
                }
                ch = (char)next;
                if (!char.IsDigit(ch))
                {
                    break;
                }
                sb.Append((char)reader.Read());
            }
        }
        else
        {
            throw new ParseException($"Неожиданный символ '{ch}' при ожидании числа.");
        }

        if (int.TryParse(sb.ToString(), out int value))
        {
            return new NumberExpression(value);
        }
        else
        {
            throw new ParseException($"Некорректный формат числа: {sb}");
        }
    }

    /// <summary>
    /// Пропускает пробельные символы во входных данных.
    /// </summary>
    private void SkipWhiteSpace()
    {
        while (true)
        {
            int next = reader.Peek();
            if (next == -1)
            {
                break;
            }
            if (!char.IsWhiteSpace((char)next))
            {
                break;
            }
            reader.Read();
        }
    }
}
