using System;

namespace Lab02
{
    public class Task3
    {
        public static void Run()
        {
            string[] days = { "Понеділок", "Вівторок", "Середа", "Четвер", "П'ятниця", "Субота", "Неділя" };
            int[] patients = new int[7];

            for (int i = 0; i < 7; i++)
            {
                patients[i] = int.Parse(Console.ReadLine()!);
            }

            int total = 0;
            int maxIdx = 0;
            int minIdx = 0;

            for (int i = 0; i < 7; i++)
            {
                total += patients[i];
                if (patients[i] > patients[maxIdx])
                {
                    maxIdx = i;
                }
                if (patients[i] < patients[minIdx])
                {
                    minIdx = i;
                }
            }

            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"{days[i],-11} : {patients[i]} пацієнтів");
            }

            Console.WriteLine($"Разом:      {total}");
            Console.WriteLine($"Найбільше:  {days[maxIdx]} ({patients[maxIdx]})");
            Console.WriteLine($"Найменше:   {days[minIdx]} ({patients[minIdx]})");
        }
    }
}