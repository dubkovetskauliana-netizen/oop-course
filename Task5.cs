using System;

namespace oop_course
{
    public static class Task5
    {
        public static void Run()
        {
            // просимо ввести число від 1 до 7
            Console.Write("Введіть номер дня тижня (1-7): ");
            string vh = Console.ReadLine()!;
            int day = int.Parse(vh);

            // використовуємо switch як вираз (switch expression) за умовою задачі
            // знак _ виконує роль гілки-пастки discard pattern
            string info = day switch
            {
            1 => "Понеділок, 08:00–18:00",
            2 => "Вівторок, 08:00–18:00",
            3 => "Середа, 09:00–17:00",
            4 => "Четвер, 08:00–18:00",
            5 => "П'ятниця, 08:00–16:00",
            6 => "Субота, 09:00–14:00",
            7 => "Неділя - вихідний",
            _ => "невідомий день"
            };

            // виводимо фінальний сформований рядок
            Console.WriteLine($"День: {info}");
        }
    }
}