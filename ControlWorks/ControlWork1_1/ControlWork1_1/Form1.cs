namespace ControlWork1_1;

/// <summary>
/// Главная форма приложения с одной «бегающей» кнопкой.
/// Кнопка ускользает от курсора при наведении на неё.
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
    /// Внутренний отступ (в пикселях) между кнопкой и границами
    /// клиентской области формы.
    /// </summary>
    private const int Padding = 20;

    /// <summary>
    /// Инициализирует экземпляр <see cref="Form1"/>.
    /// Настраивает минимальный размер формы, подписывается
    /// на мышиные события и событие изменения размера.
    /// </summary>
    public Form1()
    {
        InitializeComponent();

        int minClientWidth = runawayButton.Width + Padding * 2;
        int minClientHeight = runawayButton.Height + Padding * 2;
        Size chrome = Size - ClientSize;

        MinimumSize = new Size(
            Math.Max(minClientWidth + chrome.Width, 400),
            Math.Max(minClientHeight + chrome.Height, 400)
        );

        runawayButton.MouseEnter += HandleButtonMouseEnter;

        runawayButton.Click += (_, _) => Application.Exit();

        Resize += (_, _) => EnsureButtonInsideBounds();
    }

    /// <summary>
    /// Обработчик события <see cref="Control.MouseEnter"/>.
    /// Перемещает кнопку в случайное место внутри формы,
    /// чтобы она не перекрывала курсор.
    /// </summary>
    private void HandleButtonMouseEnter(object sender, EventArgs e)
    {
        MoveButtonRandomlyInsideForm();
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
    /// после изменения размера формы, и при необходимости
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
