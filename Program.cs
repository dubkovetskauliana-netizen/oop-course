using System;

namespace oop_course
{
    class Program
    {
        static void Main(string[] args)
        {
            // Налаштування для крапки в дробових числах
            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.InvariantCulture;

            // Запуск нашого першого завдання
            Task1.Run();
        }
    }
}