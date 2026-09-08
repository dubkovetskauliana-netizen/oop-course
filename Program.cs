using System;

namespace oop_course
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.InvariantCulture;

            // Тепер запускаємо саме шосту задачу
            Task6.Run();
        }
    }
}