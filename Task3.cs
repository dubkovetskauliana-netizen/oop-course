using System;

namespace oop_course
{
    public static class Task3
    {
        public static void Run()
        {
            // просимо користувача ввести рік народження
            Console.Write("Введіть рік народження пацієнта: ");
            string vvod = Console.ReadLine()!;
            int y = int.Parse(vvod);

            // рахуємо вік від фіксованого 2026 року за умовою задачі
            int age = 2026 - y;

            // виводимо перший рядок результату
            Console.WriteLine($"Вік: {age} р.");

            // визначаємо категорію через перевірку умов
            if (age <= 17)
            {
                Console.WriteLine("Категорія: дитина");
            }
            else if (age <= 59)
            {
                Console.WriteLine("Категорія: дорослий");
            }
            else
            {
                Console.WriteLine("Категорія: пенсіонер");
            }
        }
    }
}