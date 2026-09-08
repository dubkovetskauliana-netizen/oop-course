using System;

namespace oop_course
{
    class Program
    {
        static void Main(string[] args)
        {
            // Налаштування для крапки в числах
            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.InvariantCulture;

            // Тепер запускаємо саме ДРУГУ задачу
            Task2.Run();
        }
    }
}