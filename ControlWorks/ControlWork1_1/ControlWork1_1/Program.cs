using System;
using System.Windows.Forms;

namespace ControlWork1_1;

/// <summary>
/// Класс‑загрузчик. Настраивает WinForms‑окружение и
/// показывает главную форму <see cref="Form1"/>.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Главный вход в приложение.
    /// Размечен <see cref="STAThreadAttribute"/>, так как
    /// WinForms требует однопоточный apartment‑модель COM.
    /// </summary>
    [STAThread]
    private static void Main()
    {

        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
}
