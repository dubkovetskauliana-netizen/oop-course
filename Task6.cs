using System;

namespace Lab02
{
    public class Task6
    {
        public static void Run()
        {
            int n = int.Parse(Console.ReadLine()!);
            int[][] doctors = new int[n][];

            for (int i = 0; i < n; i++)
            {
                int k = int.Parse(Console.ReadLine()!);
                doctors[i] = new int[k];
                for (int j = 0; j < k; j++)
                {
                    doctors[i][j] = int.Parse(Console.ReadLine()!);
                }
            }

            int bestDoctorIndex = 0;
            int maxTotalIncome = -1;

            for (int i = 0; i < n; i++)
            {
                int count = doctors[i].Length;
                int sum = 0;
                for (int j = 0; j < count; j++)
                {
                    sum += doctors[i][j];
                }

                double average = count > 0 ? (double)sum / count : 0;

                Console.WriteLine($"Лікар {i + 1}: {count} прийоми, сума={sum} грн, середня={average:F2} грн");

                if (sum > maxTotalIncome)
                {
                    maxTotalIncome = sum;
                    bestDoctorIndex = i;
                }
            }

            Console.WriteLine($"Найбільший дохід: Лікар {bestDoctorIndex + 1} ({maxTotalIncome} грн)");
        }
    }
}