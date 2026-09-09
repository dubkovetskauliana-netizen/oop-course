using System;

namespace oop_course
{
    public static class Task4
    {
        public static void Run()
        {
            // зчитуємо перший показник - систолічний тиск
            Console.Write("Введіть систолічний тиск: ");
            string v1 = Console.ReadLine()!;
            int s = int.Parse(v1);

            // зчитуємо другий показник - діастолічний тиск
            Console.Write("Введіть діастолічний тиск: ");
            string v2 = Console.ReadLine()!;
            int d = int.Parse(v2);

            // змінна для збереження статусу
            string status;
            // перевіряємо умови по порядку від норми до гіпертонії
            if (s < 120 && d < 80)
            {
                status = "норма";
            }
            else if (s < 130 && d < 80)
            {
                status = "підвищений";
            }
            else if (s < 140 || d < 90)
            {
                status = "гіпертонія 1 ступеня";
            }
            else
            {
                status = "гіпертонія 2 ступеня";
            }

            // виводимо фінальний рядок за шаблоном з методички
            Console.WriteLine($"Тиск: {s}/{d} - {status}");
        }
    }
}