using System;

namespace oop_course
{
    public static class Task2
    {
        public static void Run()
        {
            // зчитуємо ціну прийому
            Console.Write("Введіть ціну прийому (грн): ");
            string s1 = Console.ReadLine()!;
            double price = double.Parse(s1);

            // зчитуємо кількість
            Console.Write("Введіть кількість прийомів: ");
            string s2 = Console.ReadLine()!;
            int count = int.Parse(s2);

            // зчитуємо знижку
            Console.Write("Введіть знижку (%): ");
            string s3 = Console.ReadLine()!;
            int disc = int.Parse(s3);

            // рахуємо коефіцієнт знижки
            double koef = 1.0 - (disc / 100.0);
            // рахуємо загальну суму
            double total = price * count * koef;

            // виводимо результат
            Console.WriteLine($"Сума: {total:F2} грн");
        }
    }
}