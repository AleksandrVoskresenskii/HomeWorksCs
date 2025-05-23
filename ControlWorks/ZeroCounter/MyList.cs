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

using System.Collections;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Простейший односвязный список с добавлением в конец.
/// </summary>
/// <typeparam name="T">Тип данных.</typeparam>
public sealed class MyList<T> : IEnumerable<T>
{
    private Node? head;
    private Node? tail;

    /// <summary>
    /// Добавляет значение в конец списка.
    /// </summary>
    /// <param name="value">Значение, которое добавляется в список.</param>
    public void AddLast(T value)
    {
        var node = new Node(value);
        if (this.head is null)
        {
            this.head = this.tail = node;
        }
        else
        {
            this.tail!.Next = node;
            this.tail = node;
        }
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        for (var cur = this.head; cur is not null; cur = cur.Next)
        {
            yield return cur.Value;
        }
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    // Внутренний узел списка (инкапсулируем).
    private sealed class Node
    {
        public Node(T value) => this.Value = value;

        public T Value { get; }

        public Node? Next { get; set; }
    }
}
