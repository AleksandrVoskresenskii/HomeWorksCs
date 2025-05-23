namespace ZeroCounter;

/// <summary>
/// Определяет правило, по которому элемент считается «нулевым».
/// </summary>
/// <typeparam name="T">Тип элементов списка.</typeparam>
public interface INullChecker<T>
{
    /// <summary>
    /// Проверяет, считается ли <paramref name="value"/> «нулевым» по заданному правилу.
    /// </summary>
    /// <param name="value">Элемент, который проверяется на «нуль».</param>
    /// <returns>
    /// <c>true</c>, если <paramref name="value"/> соответствует понятию «нулевого»;
    /// иначе <c>false</c>.
    /// </returns>
    bool IsNull(T value);
}
