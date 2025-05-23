// MIT License
//
// Copyright (c) 2025 AleksandrVoskresenskii
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction...
//
// Full license text is available in the LICENSE file.

namespace ZeroCounter;

/// <summary>
/// Утилиты для работы со списками.
/// </summary>
public static class ZeroUtils
{
    /// <summary>
    /// Считает количество «нулевых» элементов в <paramref name="list"/>.
    /// </summary>
    /// <typeparam name="T">Тип элемента списка.</typeparam>
    /// <param name="list">Список, который обходится.</param>
    /// <param name="checker">Объект, определяющий «нуль».</param>
    /// <returns>Число элементов, распознанных как «нулевые».</returns>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="list"/> или <paramref name="checker"/> равны <c>null</c>.
    /// </exception>
    public static int CountZero<T>(MyList<T> list, INullChecker<T> checker)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(checker);

        var count = 0;
        foreach (var item in list)
        {
            if (checker.IsNull(item))
            {
                count++;
            }
        }

        return count;
    }
}
