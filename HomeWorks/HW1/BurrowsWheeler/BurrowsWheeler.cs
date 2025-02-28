using System;
using System.Linq;

namespace BWT;

public static class BurrowsWheelerTransform
{
    public static (string transformed, int index) Transform(string input)
    {
        if (!input.EndsWith("$"))
        {
            input += '$'; // Добавляем уникальный терминатор
        }

        int n = input.Length;
        int[] indices = Enumerable.Range(0, n).ToArray(); // Создаём массив индексов

        // Сортируем индексы, используя кастомный компаратор
        Array.Sort(indices, (i, j) =>
        {
            for (int k = 0; k < n; k++)
            {
                char ci = input[(i + k) % n];
                char cj = input[(j + k) % n];
                if (ci != cj)
                {
                    return ci - cj;
                }
            }

            return 0;
        });

        // Формируем BWT-строку и находим позицию исходной строки
        char[] lastColumn = new char[n];
        int originalIndex = -1;

        for (int i = 0; i < n; i++)
        {
            lastColumn[i] = input[(indices[i] + n - 1) % n];
            if (indices[i] == 0)
            {
                originalIndex = i;
            }
        }

        return (new string(lastColumn), originalIndex);
    }

    public static string InverseTransform(string bwt, int index)
    {
        int n = bwt.Length;
        int[] count = new int[256]; // Массив для подсчёта частот символов
        int[] firstOccurrence = new int[256];

        // Подсчитываем частоты символов
        foreach (char c in bwt)
        {
            count[c]++;
        }

        // Определяем первое вхождение каждого символа в отсортированной версии строки
        int sum = 0;
        for (int i = 0; i < 256; i++)
        {
            firstOccurrence[i] = sum;
            sum += count[i];
        }

        // Строим массив next
        int[] next = new int[n];
        int[] tempCount = new int[256];

        for (int i = 0; i < n; i++)
        {
            char c = bwt[i];
            next[i] = firstOccurrence[c] + tempCount[c];
            tempCount[c]++;
        }

        // Восстанавливаем исходную строку
        char[] original = new char[n];
        int pos = index;

        for (int i = n - 1; i >= 0; i--)
        {
            original[i] = bwt[pos];
            pos = next[pos];
        }

        return new string(original).TrimEnd('$'); // Убираем терминатор '$'
    }
}