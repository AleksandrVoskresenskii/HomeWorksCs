// Файл: PriorityQueue.cs
using System;
using System.Collections.Generic;

namespace PriorityQueueProject;

/// <summary>
/// Реализует очередь с приоритетами, позволяющую добавлять элементы с приоритетом и извлекать
/// элемент с наивысшим приоритетом. При равных приоритетах элементы извлекаются в порядке добавления.
/// </summary>
/// <typeparam name="T">Тип хранимых элементов.</typeparam>
public class PriorityQueue<T>
{
    // В качестве контейнера используем SortedDictionary для хранения наших очередей по каждому приоритету.
    // Компаратор сортирует ключи по убыванию – таким образом, наивысший приоритет будет первым.
    private readonly SortedDictionary<int, SimpleQueue> _queues =
        new SortedDictionary<int, SimpleQueue>(Comparer<int>.Create((x, y) => y.CompareTo(x)));

    /// <summary>
    /// Возвращает признак того, что очередь пуста.
    /// </summary>
    public bool Empty => _queues.Count == 0;

    /// <summary>
    /// Добавляет элемент в очередь с заданным приоритетом.
    /// </summary>
    /// <param name="item">Элемент, который добавляется.</param>
    /// <param name="priority">Числовой приоритет элемента.</param>
    public void Enqueue(T item, int priority)
    {
        if (!_queues.TryGetValue(priority, out var queue))
        {
            queue = new SimpleQueue();
            _queues.Add(priority, queue);
        }
        queue.Enqueue(item);
    }

    /// <summary>
    /// Извлекает и удаляет элемент с наивысшим приоритетом.
    /// Если несколько элементов имеют одинаковый приоритет, извлечение происходит по принципу FIFO.
    /// </summary>
    /// <returns>Извлечённый элемент.</returns>
    /// <exception cref="InvalidOperationException">Выбрасывается, если очередь пуста.</exception>
    public T Dequeue()
    {
        if (Empty)
        {
            throw new InvalidOperationException("Очередь пуста.");
        }

        // Проходим по очередям, сортированным по убыванию приоритета.
        foreach (var pair in _queues)
        {
            var queue = pair.Value;
            T item = queue.Dequeue();
            if (queue.Count == 0)
            {
                _queues.Remove(pair.Key);
            }
            return item;
        }

        throw new InvalidOperationException("Очередь пуста.");
    }

    /// <summary>
    /// Вспомогательный класс для реализации FIFO‑очереди без использования стандартного Queue.
    /// </summary>
    private class SimpleQueue
    {
        /// <summary>
        /// Узел очереди.
        /// </summary>
        private class Node
        {
            public T Value { get; }
            public Node? Next { get; set; }

            /// <summary>
            /// Инициализирует узел с указанным значением.
            /// </summary>
            /// <param name="value">Хранимое значение.</param>
            public Node(T value)
            {
                Value = value;
                Next = null;
            }
        }

        private Node? head;
        private Node? tail;

        /// <summary>
        /// Количество элементов в очереди.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Добавляет элемент в конец очереди.
        /// </summary>
        /// <param name="item">Элемент, который добавляется.</param>
        public void Enqueue(T item)
        {
            var newNode = new Node(item);
            if (tail is null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                tail = newNode;
            }
            Count++;
        }

        /// <summary>
        /// Извлекает и удаляет элемент из начала очереди.
        /// </summary>
        /// <returns>Извлечённый элемент.</returns>
        /// <exception cref="InvalidOperationException">Выбрасывается, если очередь пуста.</exception>
        public T Dequeue()
        {
            if (head is null)
            {
                throw new InvalidOperationException("Очередь пуста.");
            }

            T value = head.Value;
            head = head.Next;
            if (head is null)
            {
                tail = null;
            }
            Count--;
            return value;
        }
    }
}