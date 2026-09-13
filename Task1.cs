using System;

namespace Lab02
{
    public class Task1
    {
        public static void Run()
        {
            int count = int.Parse(Console.ReadLine()!);
            double[] arr = new double[count];

            for (int i = 0; i < count; i++)
            {
                arr[i] = double.Parse(Console.ReadLine()!);
            }

            double total = 0;
            double min = arr[0];
            double max = arr[0];

            for (int i = 0; i < arr.Length; i++)
            {
                total += arr[i];
                if (arr[i] < min) min = arr[i];
                if (arr[i] > max) max = arr[i];
            }

            double avg = total / count;

            int aboveAvgCount = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > avg)
                {
                    aboveAvgCount++;
                }
            }

            Console.WriteLine($"Кількість: {count} / Середня вага: {avg:F1} кг / Мін / Макс: {min} / {max} кг / Вище середнього: {aboveAvgCount} з {count}");
        }
    }
}