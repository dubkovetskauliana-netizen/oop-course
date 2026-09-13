using System;

namespace ClinicApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PatientManager manager = new PatientManager();

            // Початкове заповнення для перевірки
            manager.Add(new Patient("Іван", "Петренко", new DateTime(1985, 5, 12), "A+", "0501234567"));
            manager.Add(new Patient("Олена", "Коваль", new DateTime(1993, 8, 20), "B-", "0672345678"));
            manager.Add(new Patient("Максим", "Бойко", new DateTime(2010, 2, 10), "O+", "0933456789"));
            manager.Add(new Patient("Марія", "Ткач", new DateTime(2000, 1, 1), "Невідомо", "0000000000"));

            RunMenu(manager);
        }

        static void RunMenu(PatientManager manager)
        {
            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ ПАЦІЄНТІВ ---");
                Console.WriteLine("1. Показати всіх");
                Console.WriteLine("2. Додати пацієнта");
                Console.WriteLine("3. Знайти за ім'ям/прізвищем");
                Console.WriteLine("4. Видалити за ID");
                Console.WriteLine("5. Статистика");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть опцію: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        manager.DisplayAll();
                        break;
                    case "2":
                        AddPatientMenu(manager);
                        break;
                    case "3":
                        SearchPatientMenu(manager);
                        break;
                    case "4":
                        RemovePatientMenu(manager);
                        break;
                    case "5":
                        manager.DisplayStats();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                        break;
                }
            }
        }

        static void AddPatientMenu(PatientManager manager)
        {
            Console.Write("Введіть ім'я: ");
            string firstName = Console.ReadLine() ?? "Невідомо";

            Console.Write("Введіть прізвище: ");
            string lastName = Console.ReadLine() ?? "Невідомо";

            manager.Add(new Patient(firstName, lastName));
        }

        static void SearchPatientMenu(PatientManager manager)
        {
            Console.Write("Введіть пошуковий запит (ім'я або прізвище): ");
            string query = Console.ReadLine() ?? "";

            Patient[] found = manager.FindByName(query);
            if (found.Length == 0)
            {
                Console.WriteLine("Нікого не знайдено.");
            }
            else
            {
                Console.WriteLine($"Знайдено пацієнтів: {found.Length}");
                foreach (var p in found)
                {
                    Console.WriteLine(p);
                }
            }
        }

        static void RemovePatientMenu(PatientManager manager)
        {
            Console.Write("Введіть ID пацієнта для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool success = manager.Remove(id);
                if (success)
                {
                    Console.WriteLine($"Пацієнта з ID {id} успішно видалено.");
                }
                else
                {
                    Console.WriteLine($"Пацієнта з ID {id} не знайдено.");
                }
            }
            else
            {
                Console.WriteLine("Некоректний ID.");
            }
        }
    }
}