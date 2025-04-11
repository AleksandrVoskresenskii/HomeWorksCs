using PriorityQueueProject;

var queue = new PriorityQueue<string>();

// Добавляем элементы с различными приоритетами.
queue.Enqueue("Элемент с приоритетом 1", 1);
queue.Enqueue("Элемент с приоритетом 3", 3);
queue.Enqueue("Элемент с приоритетом 2", 2);
queue.Enqueue("Второй элемент с приоритетом 3", 3);

Console.WriteLine("Демонстрация работы очереди с приоритетами:");
while (!queue.Empty)
{
    Console.WriteLine(queue.Dequeue());
}