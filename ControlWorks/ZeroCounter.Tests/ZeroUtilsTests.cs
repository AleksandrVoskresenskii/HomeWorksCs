// MIT License
//
// Copyright (c) 2025 AleksandrVoskresenskii
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction...
//
// Full license text is available in the LICENSE file.

namespace ZeroCounter.Tests;

using Xunit;
using ZeroCounter;

/// <summary>
/// Набор юнит-тестов для <see cref="ZeroUtils"/>.
/// </summary>
public sealed class ZeroUtilsTests
{
    /// <summary>
    /// Проверяет подсчёт «нулевых» элементов для списка целых чисел.
    /// </summary>
    [Fact]
    public void CountZero_Ints_Works()
    {
        var list = new MyList<int>();
        list.AddLast(0);
        list.AddLast(5);
        list.AddLast(0);

        var result = ZeroUtils.CountZero(list, new IntZeroChecker());

        Assert.Equal(2, result);
    }

    /// <summary>
    /// Проверяет подсчёт «нулевых» элементов для списка строк (null или пустая строка).
    /// </summary>
    [Fact]
    public void CountZero_Strings_Works()
    {
        var list = new MyList<string?>();
        list.AddLast(string.Empty);
        list.AddLast("abc");
        list.AddLast(null);

        var result = ZeroUtils.CountZero(list, new EmptyStringChecker());

        Assert.Equal(2, result);
    }

    /// <summary>
    /// Убеждаемся, что перечисление элементов возвращает их в порядке добавления.
    /// </summary>
    [Fact]
    public void Enumerator_Preserves_Order()
    {
        var list = new MyList<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);

        Assert.Equal(new[] { 1, 2, 3 }, list.ToArray());
    }

    /// <summary>
    /// Реализация <see cref="INullChecker{T}"/> для целых чисел.
    /// </summary>
    private sealed class IntZeroChecker : INullChecker<int>
    {
        public bool IsNull(int value) => value == 0;
    }

    /// <summary>
    /// Реализация <see cref="INullChecker{T}"/> для строк (null или пустая строка).
    /// </summary>
    private sealed class EmptyStringChecker : INullChecker<string?>
    {
        public bool IsNull(string? value) => string.IsNullOrEmpty(value);
    }
}

