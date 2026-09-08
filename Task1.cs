using System;

namespace oop_course
{
    public static class Task1
    {
        public static void Run()
        {
            // спочатку просимо ввести вагу в кілограмах
            Console.Write("Введіть вагу пацієнта (кг): ");
            string vstr = Console.ReadLine()!;
            double w = double.Parse(vstr);

            // тепер просимо ввести зріст у метрах
            Console.Write("Введіть зріст пацієнта (м): ");
            string zstr = Console.ReadLine()!;
            double h = double.Parse(zstr);

            // рахуємо індекс за формулою з умови задачі
            // зріст на зріст обов'язково беремо в дужки для пріоритету
            double imt = w / (h * h);

            // виводимо відповідь з форматом F2 (2 знаки після коми)
            Console.WriteLine($"IMT: {imt:F2}");
        }
    }
}