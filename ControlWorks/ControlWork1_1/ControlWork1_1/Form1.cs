using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace ControlWork1_1;

/// <summary>
/// Главная форма приложения с одной «бегающей» кнопкой.
/// Кнопка ускользает от курсора мыши, если тот приближается ближе
/// заданного радиуса (<see cref="TriggerDistancePx"/>);
/// По клику завершает работу приложения.
/// </summary>
public partial class Form1 : Form
{
    /// <summary>
    /// Генератор псевдослучайных чисел для выбора новой позиции
    /// кнопки.
    /// </summary>
    private readonly Random rand = new();

    /// <summary>
    /// Внутренний отступ (в пикселях) между кнопкой и границами
    /// клиентской области формы.
    /// </summary>
    private const int Padding = 20;

    /// <summary>
    /// Радиус (в пикселях), внутри которого кнопка должна
    /// «убежать» от курсора.
    /// </summary>
    private const int TriggerDistancePx = 140;

    /// <summary>
    /// Инициализирует экземпляр <see cref="Form1"/>.
    /// Настраивает минимальный размер формы, подписывается
    /// на мышиные события и событие изменения размера.
    /// </summary>
    public Form1()
    {
        InitializeComponent();

        int minClientWidth = runawayButton.Width + Padding * 2;
        int minClientHeight = runawayButton.Height + Padding * 2;

        Size chrome = Size - ClientSize;

        MinimumSize = new Size(
            minClientWidth + chrome.Width,
            minClientHeight + chrome.Height);

        MouseMove += HandleMouseMove;
        runawayButton.MouseMove += HandleMouseMove;

        runawayButton.Click += (_, _) => Application.Exit();

        Resize += (_, _) => EnsureButtonInsideBounds();
    }

    /// <summary>
    /// Обработчик движения мыши. Проверяет дистанцию до кнопки и,
    /// при необходимости, перемещает кнопку в случайное место.
    /// </summary>
    /// <param name="sender">Источник события (форма или кнопка).</param>
    /// <param name="e">Параметры события <see cref="MouseEventArgs"/>.</param>
    private void HandleMouseMove(object? sender, MouseEventArgs e)
    {
        Point mouse = e.Location;

        Point center = new(
            runawayButton.Left + runawayButton.Width / 2,
            runawayButton.Top + runawayButton.Height / 2);

        // Расстояние между курсором и центром кнопки
        double distance = Math.Sqrt(Math.Pow(center.X - mouse.X, 2) + Math.Pow(center.Y - mouse.Y, 2));

        if (distance < TriggerDistancePx)
        {
            MoveButtonRandomlyInsideForm();
        }
    }

    /// <summary>
    /// Перемещает кнопку в случайное место, гарантируя,
    /// что она целиком остаётся в пределах <see cref="ClientSize"/>.
    /// </summary>
    private void MoveButtonRandomlyInsideForm()
    {
        int maxX = ClientSize.Width - runawayButton.Width - Padding;
        int maxY = ClientSize.Height - runawayButton.Height - Padding;

        Point mousePosition = PointToClient(Cursor.Position);

        int safeAreaWidth = runawayButton.Width + Padding * 2;
        int safeAreaHeight = runawayButton.Height + Padding * 2;

        int newLeft = rand.Next(Padding, maxX);
        int newTop = rand.Next(Padding, maxY);

        while (Math.Abs(mousePosition.X - newLeft) < safeAreaWidth && Math.Abs(mousePosition.Y - newTop) < safeAreaHeight)
        {
            newLeft = rand.Next(Padding, maxX);
            newTop = rand.Next(Padding, maxY);
        }

        runawayButton.Left = newLeft;
        runawayButton.Top = newTop;
    }

    /// <summary>
    /// Проверяет, что кнопка остаётся внутри области окна
    /// после изменения размера формы, и при необходимости
    /// передвигает её внутрь.
    /// </summary>
    private void EnsureButtonInsideBounds()
    {
        int maxX = ClientSize.Width - runawayButton.Width - Padding;
        int maxY = ClientSize.Height - runawayButton.Height - Padding;

        runawayButton.Left = Math.Min(runawayButton.Left, Math.Max(Padding, maxX));
        runawayButton.Top = Math.Min(runawayButton.Top, Math.Max(Padding, maxY));
    }
}
