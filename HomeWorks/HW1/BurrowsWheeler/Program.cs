using System;
using BWT; // Импортируем пространство имён с реализацией BWT

Console.Write("Введите строку: ");
string input = Console.ReadLine();

// Прямое преобразование: получаем BWT-строку и позицию исходного сдвига
var (bwt, index) = BurrowsWheelerTransform.Transform(input);
Console.WriteLine($"BWT: {bwt}");
Console.WriteLine($"Позиция исходной строки: {index}");

// Обратное преобразование: восстанавливаем исходную строку
string restored = BurrowsWheelerTransform.InverseTransform(bwt, index);
Console.WriteLine($"Восстановленная строка: {restored}");

// Проверка корректности восстановления
Console.WriteLine(restored == input 
    ? "Восстановление успешно!" 
    : "Ошибка восстановления.");