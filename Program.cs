using System;

namespace oop_course
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.InvariantCulture;

            // Запускаємо третю задачу
            Task3.Run();
        }
    }
}