using System;

namespace Lab02
{
    public class Task8
    {
        public static void Run()
        {
            int d = int.Parse(Console.ReadLine()!);
            int w = int.Parse(Console.ReadLine()!);

            int[,,] data = new int[d, w, 2];

            for (int i = 0; i < d; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        data[i, j, k] = int.Parse(Console.ReadLine()!);
                    }
                }
            }

            int[] departmentTotals = new int[d];
            int maxPatients = -1;
            int mostLoadedDept = 0;

            for (int i = 0; i < d; i++)
            {
                Console.WriteLine($"Відділення {i + 1}:");
                int totalDeptPatients = 0;

                for (int j = 0; j < w; j++)
                {
                    int morning = data[i, j, 0];
                    int evening = data[i, j, 1];
                    int weekSum = morning + evening;
                    totalDeptPatients += weekSum;

                    Console.WriteLine($"  Тиждень {j + 1}: ранок {morning}, вечір {evening} -> разом {weekSum}");
                }

                departmentTotals[i] = totalDeptPatients;
                Console.WriteLine($"Разом: {totalDeptPatients} пацієнтів");

                if (totalDeptPatients > maxPatients)
                {
                    maxPatients = totalDeptPatients;
                    mostLoadedDept = i + 1;
                }
            }

            Console.WriteLine($"Найзавантаженіше: Відділення {mostLoadedDept} ({maxPatients} пацієнтів)");
        }
    }
}