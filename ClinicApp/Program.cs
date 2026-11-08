using ClinicApp.Models;
using ClinicApp.Utils;
using System;
using System.Globalization;
using System.Threading;

namespace ClinicApp;

class Program
{
    static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Clinic clinic = new Clinic();

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
            Console.WriteLine("|| 6. Звіт         – загальна статистика      ||");
            Console.WriteLine("|| 9. Плани лікування – рефлексія, атрибути   ||");
            Console.WriteLine("|| 0. Вийти                                   ||");
            Console.WriteLine("================================================");
            Console.Write("Оберіть розділ: ");

            string choice = Console.ReadLine();
            if (choice == "0") break;

            switch (choice)
            {
                case "9":
                    TreatmentPlansMenu(clinic);
                    break;
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                    Console.WriteLine("Цей розділ працює у стандартному режимі.");
                    break;
                default:
                    Console.WriteLine("Некоректний вибір.");
                    break;
            }
        }
    }

    private static void TreatmentPlansMenu(Clinic clinic)
    {
        while (true)
        {
            Console.WriteLine("\n--- Плани лікування ---");
            Console.WriteLine("1. Показати всі плани");
            Console.WriteLine("2. Додати план лікування");
            Console.WriteLine("3. Плани пацієнта");
            Console.WriteLine("4. Активувати план");
            Console.WriteLine("5. Завершити план");
            Console.WriteLine("6. Скасувати план");
            Console.WriteLine("7. Інформація про тип TreatmentPlan");
            Console.WriteLine("8. Назад");
            Console.Write("Оберіть дію: ");

            string choice = Console.ReadLine();
            if (choice == "8") break;

            switch (choice)
            {
                case "1":
                    {
                        TreatmentPlan[] allPlans = clinic.TreatmentPlans.GetAll();
                        if (allPlans.Length == 0) Console.WriteLine("Планів лікування ще немає.");
                        foreach (var p in allPlans)
                        {
                            Console.WriteLine($"ID: {p.Id} | Пацієнт ID: {p.PatientId} | Діагноз: {p.Diagnosis} | Статус: {p.Status}");
                        }
                    }
                    break;

                case "2":
                    {
                        TreatmentPlan newPlan = FormBuilder.Build<TreatmentPlan>();
                        if (clinic.TreatmentPlans.Add(newPlan))
                        {
                            Console.WriteLine($"План лікування успішно створено! ID: {newPlan.Id}");
                        }
                    }
                    break;

                case "3":
                    {
                        Console.Write("ID пацієнта: ");
                        if (int.TryParse(Console.ReadLine(), out int pId))
                        {
                            TreatmentPlan[] pPlans = clinic.TreatmentPlans.GetByPatient(pId);
                            if (pPlans.Length == 0) Console.WriteLine("У цього пацієнта немає планів.");
                            foreach (var p in pPlans)
                            {
                                Console.WriteLine($"ID: {p.Id} | Діагноз: {p.Diagnosis} | Статус: {p.Status}");
                            }
                        }
                    }
                    break;

                case "4":
                    {
                        Console.Write("ID плану: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            TreatmentPlan plan = clinic.TreatmentPlans.GetById(id);
                            if (plan == null)
                            {
                                Console.WriteLine("План не знайдено");
                            }
                            else if (!plan.Activate())
                            {
                                Console.WriteLine("Перехід неможливий");
                            }
                            else
                            {
                                Console.WriteLine("План успішно активовано!");
                            }
                        }
                    }
                    break;

                case "5":
                    {
                        Console.Write("ID плану: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            TreatmentPlan plan = clinic.TreatmentPlans.GetById(id);
                            if (plan == null)
                            {
                                Console.WriteLine("План не знайдено");
                            }
                            else if (!plan.Complete())
                            {
                                Console.WriteLine("Перехід неможливий");
                            }
                            else
                            {
                                Console.WriteLine("План успішно завершено!");
                            }
                        }
                    }
                    break;

                case "6":
                    {
                        Console.Write("ID плану: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            TreatmentPlan plan = clinic.TreatmentPlans.GetById(id);
                            if (plan == null)
                            {
                                Console.WriteLine("План не знайдено");
                            }
                            else if (!plan.Cancel())
                            {
                                Console.WriteLine("Перехід неможливий");
                            }
                            else
                            {
                                Console.WriteLine("План успішно скасовано!");
                            }
                        }
                    }
                    break;

                case "7":
                    ModelValidator.PrintInfo(typeof(TreatmentPlan));
                    break;

                default:
                    Console.WriteLine("Некоректний варіант.");
                    break;
            }
        }
    }
}
