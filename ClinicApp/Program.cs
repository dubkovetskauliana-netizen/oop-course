using System;
using System.Globalization;
using System.Threading;
using ClinicApp.Models;
using ClinicApp.Managers;

namespace ClinicApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Clinic clinic = new Clinic();
            SeedData(clinic);

            while (true)
            {
                Console.WriteLine("================================================");
                Console.WriteLine("||              МЕДИЧНА КЛІНІКА               ||");
                Console.WriteLine("================================================");
                Console.WriteLine("|| 1. Пацієнти     – реєстрація, пошук        ||");
                Console.WriteLine("|| 2. Лікарі       – персонал, розклад        ||");
                Console.WriteLine("|| 3. Записи       – прийоми, скасування      ||");
                Console.WriteLine("|| 4. Медична картка – діагнози, рецепти      ||");
                Console.WriteLine("|| 5. Рахунки      – оплата, борги            ||");
                Console.WriteLine("|| 6. Черга        – очікування, прийом       ||");
                Console.WriteLine("|| 7. Звіт         – загальна статистика      ||");
                Console.WriteLine("|| 0. Вийти                                   ||");
                Console.WriteLine("================================================");
                Console.Write("Оберіть розділ: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                    case "4":
                        Console.WriteLine("Розділ працює у фоновому режимі з минулих лаб.");
                        break;
                    case "2":
                        DoctorsMenu(clinic);
                        break;
                    case "3":
                        AppointmentsMenu(clinic);
                        break;
                    case "5":
                        BillingMenu(clinic);
                        break;
                    case "6":
                        WaitingRoomMenu(clinic);
                        break;
                    case "7":
                        Console.WriteLine("Розділ звітів працює у фоновому режимі.");
                        break;
                    default:
                        Console.WriteLine("Некоректний вибір.");
                        break;
                }
            }
        }

        private static void WaitingRoomMenu(Clinic clinic)
        {
            while (true)
            {
                Console.WriteLine("\n--- Черга очікування ---");
                Console.WriteLine("1. Додати пацієнта до черги");
                Console.WriteLine("2. Прийняти першого");
                Console.WriteLine("3. Хто перший?");
                Console.WriteLine("4. Переглянути всю чергу");
                Console.WriteLine("0. Назад");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                if (choice == "1")
                {
                    Console.Write("ID пацієнта (ціле): ");
                    if (int.TryParse(Console.ReadLine(), out int pId))
                    {
                        Patient foundPatient = clinic.Patients.FindById(pId);
                        if (foundPatient != null)
                        {
                            clinic.WaitingRoom.Enqueue(foundPatient);
                            Console.WriteLine("Пацієнта " + foundPatient.FullName + " додано до черги.");
                        }
                        else
                        {
                            Console.WriteLine("Пацієнта не знайдено");
                        }
                    }
                }
                else if (choice == "2")
                {
                    try
                    {
                        Patient head = clinic.WaitingRoom.Dequeue();
                        Console.WriteLine("Прийнято: " + head.FullName + ". У черзі лишилось: " + clinic.WaitingRoom.Count);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (choice == "3")
                {
                    try
                    {
                        Patient next = clinic.WaitingRoom.Peek();
                        Console.WriteLine("Перший у черзі: " + next.FullName);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (choice == "4")
                {
                    Patient[] array = clinic.WaitingRoom.ToArray();
                    if (array.Length == 0)
                    {
                        Console.WriteLine("Черга порожня.");
                    }
                    else
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            Console.WriteLine((i + 1) + ". " + array[i].FullName);
                        }
                    }
                }
            }
        }

        private static void SeedData(Clinic clinic)
        {
            Patient[] initialPatients = clinic.Patients.GetAll();
            for (int i = 0; i < initialPatients.Length; i++)
            {
                if (initialPatients[i] != null)
                {
                    clinic.WaitingRoom.Enqueue(initialPatients[i]);
                }
            }
        }

        private static void BillingMenu(Clinic clinic)
        {
            Console.WriteLine("Меню рахунків тимчасово недоступне у фоновому режимі.");
        }

        private static void AppointmentsMenu(Clinic clinic)
        {
            Console.WriteLine("Меню записів тимчасово недоступне у фоновому режимі.");
        }

        private static void DoctorsMenu(Clinic clinic)
        {
            Console.WriteLine("Меню лікарів тимчасово недоступне у фоновому режимі.");
        }
    }
}
