using System;

namespace Lab02
{
    public class Task2
    {
        public static void Run()
        {
            int n = int.Parse(Console.ReadLine()!);
            int[] queue = new int[n];

            for (int i = 0; i < n; i++)
            {
                queue[i] = int.Parse(Console.ReadLine()!);
            }

            // Зберігаємо початковий рядок до сортування
            string beforeSort = string.Join(" ", queue);

            // Сортування бульбашкою за зростанням
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (queue[j] > queue[j + 1])
                    {
                        int temp = queue[j];
                        queue[j] = queue[j + 1];
                        queue[j + 1] = temp;
                    }
                }
            }

            string afterSort = string.Join(" ", queue);
            int minVal = queue[0];
            int maxVal = queue[n - 1];

            Console.WriteLine($"Черга (до):     {beforeSort}");
            Console.WriteLine($"Черга (після): {afterSort}");
            Console.WriteLine($"Найдешевший:    {minVal} грн");
            Console.WriteLine($"Найдорожчий:    {maxVal} грн");
        }
    }
}