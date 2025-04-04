using ExpressionTree;

try
{
    // Чтение содержимого файла (файл file.txt должен содержать дерево разбора)
    string input = File.ReadAllText("file.txt");

    // Разбор выражения
    ExpressionParser parser = new ExpressionParser(input);
    Expression expr = parser.ParseExpression();

    // Вывод дерева разбора
    Console.WriteLine("Дерево разбора:");
    Console.WriteLine(expr.Print());

    // Вычисление выражения
    int result = expr.Evaluate();
    Console.WriteLine("Результат: " + result);
}
catch (Exception ex)
{
    // Обработка исключений, таких как ошибки разбора или деления на ноль
    Console.WriteLine("Ошибка: " + ex.Message);
}