using System;

namespace oop_course
{
    public static class Task7
    {
        public static void Run()
        {
            // зчитуємо кількість прийомів пацієнта
            Console.Write("Введіть кількість прийомів (N): ");
            string nInput = Console.ReadLine()!;
            int n = int.Parse(nInput);

            // створюємо масив для збереження вартостей типу decimal за умовою
            decimal[] prices = new decimal[n];

            // заповнюємо масив за допомогою циклу for
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Введіть вартість прийому #{i + 1}: ");
                string pInput = Console.ReadLine()!;
                prices[i] = decimal.Parse(pInput);
            }

            // --- Цикл 1: foreach для підрахунку суми, мінімуму та максимуму ---
            decimal totalSum = 0;
            decimal minPrice = prices[0];
            decimal maxPrice = prices[0];

            foreach (decimal p in prices)
            {
                totalSum += p;
                if (p < minPrice) minPrice = p;
                if (p > maxPrice) maxPrice = p;
            }

            // рахуємо середнє значення
            decimal average = totalSum / n;

            // --- Цикл 2: for для підрахунку елементів, що вищі за середнє ---
            int countAboveAverage = 0;
            for (int i = 0; i < n; i++)
            {
                if (prices[i] > average)
                {
                    countAboveAverage++;
                }
            }
            // --- Цикл 3: while для пошуку першого дорогого прийому (> 1000) ---
            int idx = 0;
            int foundIndex = -1; // -1 означає, що прийом не знайдено
            decimal foundPrice = 0;

            while (idx < n)
            {
                if (prices[idx] > 1000)
                {
                    foundIndex = idx + 1; // номер прийому (індeкс + 1)
                    foundPrice = prices[idx];
                    break; // зупиняємо пошук відразу
                }
                idx++;
            }

            // --- Вивід результатів за шаблоном з методички ---
            Console.WriteLine("\n=== Звіт по прийомах ===");
            Console.WriteLine($"Кількість:       {n}");
            Console.WriteLine($"Загальна сума:   {totalSum:F2} грн");
            Console.WriteLine($"Середня:         {average:F2} грн");
            Console.WriteLine($"Мін / Макс:      {minPrice:F2} / {maxPrice:F2} грн");
            Console.WriteLine($"Вище середнього: {countAboveAverage} з {n}");
            // логіка для виведення дорогого прийому або слова "немає"
            if (foundIndex != -1)
            {
                Console.WriteLine($"Перший > 1000:   #{foundIndex} - {foundPrice:F2} грн");
            }
            else
            {
                Console.WriteLine("Перший > 1000:   немає");
            }
        }
    }
}