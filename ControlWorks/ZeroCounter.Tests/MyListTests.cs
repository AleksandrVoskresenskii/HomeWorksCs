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

using System;
using System.Collections;
using System.Linq;
using Xunit;
using ZeroCounter;

/// <summary>
/// Набор юнит-тестов для <see cref="MyList{T}"/>.
/// </summary>
public sealed class MyListTests
{
    /// <summary>
    /// Проверяет, что новый список пустой (перечисление ни одного элемента).
    /// </summary>
    [Fact]
    public void NewList_IsEmpty()
    {
        var list = new MyList<int>();
        Assert.False(list.GetEnumerator().MoveNext());
        Assert.Empty(list);
    }

    /// <summary>
    /// Проверяет, что после одного добавления элемент доступен в перечислении.
    /// </summary>
    [Fact]
    public void AddLast_OneElement_ContainsThatElement()
    {
        var list = new MyList<string>();
        list.AddLast("hello");

        // Первым вызовом MoveNext() получаем true и Current == "hello"
        using var e = list.GetEnumerator();
        Assert.True(e.MoveNext());
        Assert.Equal("hello", e.Current);
        Assert.False(e.MoveNext());
    }

    /// <summary>
    /// Проверяет, что добавление нескольких элементов сохраняет порядок.
    /// </summary>
    [Fact]
    public void AddLast_MultipleElements_PreservesOrder()
    {
        var list = new MyList<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);

        // Можно использовать LINQ для сравнения последовательностей
        Assert.Equal(new[] { 1, 2, 3 }, list.ToArray());
    }

    /// <summary>
    /// Проверяет, что <see cref="IEnumerable"/>-интерфейс корректно работает через non-generic IEnumerator.
    /// </summary>
    [Fact]
    public void NonGenericEnumerator_Works()
    {
        var list = new MyList<char>();
        list.AddLast('a');
        list.AddLast('b');

        var nonGeneric = (IEnumerable)list;
        var e = nonGeneric.GetEnumerator();
        Assert.True(e.MoveNext());
        Assert.Equal('a', e.Current);
        Assert.True(e.MoveNext());
        Assert.Equal('b', e.Current);
        Assert.False(e.MoveNext());
    }

    /// <summary>
    /// Проверяет, что список поддерживает nullable-типы и считает null как обычный элемент.
    /// </summary>
    [Fact]
    public void AddLast_NullableType_AllowsNull()
    {
        var list = new MyList<string?>();
        list.AddLast(null);
        list.AddLast("X");

        var array = list.ToArray();
        Assert.Null(array[0]);
        Assert.Equal("X", array[1]);
    }
}

