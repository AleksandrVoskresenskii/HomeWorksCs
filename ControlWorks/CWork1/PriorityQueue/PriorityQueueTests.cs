using System;
using NUnit.Framework;
using PriorityQueueProject;

namespace PriorityQueueTestsProject;

/// <summary>
/// NUnit‑тесты для проверки очереди с приоритетами.
/// </summary>
[TestFixture]
public class PriorityQueueTests
{
    /// <summary>
    /// Проверяет работу методов Enqueue и Dequeue, а также принцип FIFO для элементов с равным приоритетом.
    /// </summary>
    [Test]
    public void TestEnqueueDequeue()
    {
        var queue = new PriorityQueue<string>();
        queue.Enqueue("Элемент 1", 1);
        queue.Enqueue("Элемент 2", 3);
        queue.Enqueue("Элемент 3", 2);
        queue.Enqueue("Элемент 4", 3);

        // Элементы с приоритетом 3 должны извлекаться в том порядке, в котором были добавлены.
        Assert.AreEqual("Элемент 2", queue.Dequeue());
        Assert.AreEqual("Элемент 4", queue.Dequeue());

        // Затем должны извлекаться элементы с оставшимися приоритетами.
        Assert.AreEqual("Элемент 3", queue.Dequeue());
        Assert.AreEqual("Элемент 1", queue.Dequeue());
        Assert.IsTrue(queue.Empty);
    }

    /// <summary>
    /// Проверяет, что извлечение из пустой очереди приводит к выбросу исключения.
    /// </summary>
    [Test]
    public void TestDequeueEmptyQueueThrows()
    {
        var queue = new PriorityQueue<int>();
        Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
    }
}