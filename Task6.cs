using System;

namespace oop_course
{
    public static class Task6
    {
        public static void Run()
        {
            // зчитуємо 5-6 значний номер медичної картки
            Console.Write("Введіть номер картки: ");
            string input = Console.ReadLine()!;
            int num = int.Parse(input);

            // знаходимо останню цифру числа за допомогою % 10
            int lastDigit = num % 10;

            // визначаємо відділення за допомогою switch expression та патерну or
            string dep = lastDigit switch
            {
                0 or 1 => "загальна терапія",
                2 or 3 => "хірургія",
                4 or 5 => "кардіологія",
                6 or 7 => "неврологія",
                8 or 9 => "офтальмологія",
                _ => "невідоме відділення"
            };
            // визначаємо пільгу та огляд за допомогою умовного (тернарного) оператора ?:
            string piga = (num % 2 == 0) ? "так" : "ні";
            string oglyad = (num % 3 == 0) ? "так" : "ні";

            // виводимо три незалежні характеристики у три рядки за шаблоном
            Console.WriteLine($"Відділення: {dep}");
            Console.WriteLine($"Пільгова:   {piga}");
            Console.WriteLine($"Огляд:      {oglyad}");
        }
    }
}