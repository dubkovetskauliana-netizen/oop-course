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

        if (clinic.Session.Exists())
        {
            Console.Write("Знайдено збережену сесію. Завантажити? (y/n): ");
            string loadAnswer = Console.ReadLine();
            if (loadAnswer != null && loadAnswer.Trim().ToLower() == "y")
            {
                int loaded = clinic.Session.Load(clinic);
                Console.WriteLine($"Завантажено {loaded} пацієнтів.");
                clinic.Logger.LogInfo($"Завантажено {loaded} пацієнтів із сесії.");
            }
        }

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
            Console.WriteLine("|| 10. Файли       – журнали, звіти, CSV      ||");
            Console.WriteLine("|| 0. Вийти                                   ||");
            Console.WriteLine("================================================");
            Console.Write("Оберіть розділ: ");

            string choice = Console.ReadLine();
            if (choice == "0")
            {
                Console.Write("Зберегти сесію? (y/n): ");
                string saveAnswer = Console.ReadLine();
                if (saveAnswer != null && saveAnswer.Trim().ToLower() == "y")
                {
                    clinic.Session.Save(clinic);
                    Console.WriteLine("Сесію збережено.");
                    clinic.Logger.LogInfo("Сесію збережено у файл.");
                }
                break;
            }

            switch (choice)
            {
                case "9":
                    TreatmentPlansMenu(clinic);
                    break;
                case "10":
                    FilesMenu(clinic);
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
                    clinic.Logger.LogWarning($"Некоректний вибір у головному меню: {choice}");
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
                        try
                        {
                            TreatmentPlan newPlan = FormBuilder.Build<TreatmentPlan>();
                            if (clinic.TreatmentPlans.Add(newPlan))
                            {
                                Console.WriteLine("План лікування успішно створено!");
                                clinic.Logger.LogInfo($"План лікування #{newPlan.Id} створено для пацієнта #{newPlan.PatientId}");
                            }
                            else
                            {
                                clinic.Logger.LogWarning("Не вдалося додати план лікування.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Помилка: {ex.Message}");
                            clinic.Logger.LogWarning($"Помилка введення: {ex.Message}");
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
                        else
                        {
                            Console.WriteLine("Некоректний ID.");
                            clinic.Logger.LogWarning("Помилка введення: некоректний ID пацієнта.");
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
                                clinic.Logger.LogWarning($"Спроба активувати неіснуючий план #{id}");
                            }
                            else if (!plan.Activate())
                            {
                                Console.WriteLine("Перехід неможливий");
                                clinic.Logger.LogWarning($"Неможливий перехід статусу при активації плану #{id}");
                            }
                            else
                            {
                                Console.WriteLine("План успішно активовано!");
                                clinic.Logger.LogInfo($"План лікування #{id} активовано");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некоректний ID.");
                            clinic.Logger.LogWarning("Помилка введення: некоректний ID плану.");
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
                                clinic.Logger.LogWarning($"Спроба завершити неіснуючий план #{id}");
                            }
                            else if (!plan.Complete())
                            {
                                Console.WriteLine("Перехід неможливий");
                                clinic.Logger.LogWarning($"Неможливий перехід статусу при завершенні плану #{id}");
                            }
                            else
                            {
                                Console.WriteLine("План успішно завершено!");
                                clinic.Logger.LogInfo($"План лікування #{id} завершено");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некоректний ID.");
                            clinic.Logger.LogWarning("Помилка введення: некоректний ID плану.");
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
                                clinic.Logger.LogWarning($"Спроба скасувати неіснуючий план #{id}");
                            }
                            else if (!plan.Cancel())
                            {
                                Console.WriteLine("Перехід неможливий");
                                clinic.Logger.LogWarning($"Неможливий перехід статусу при скасуванні плану #{id}");
                            }
                            else
                            {
                                Console.WriteLine("План скасовано.");
                                clinic.Logger.LogInfo($"План лікування #{id} скасовано");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некоректний ID.");
                            clinic.Logger.LogWarning("Помилка введення: некоректний ID плану.");
                        }
                    }
                    break;
                case "7":
                    ModelValidator.PrintInfo(typeof(TreatmentPlan));
                    break;
                default:
                    Console.WriteLine("Некоректний варіант.");
                    clinic.Logger.LogWarning($"Некоректний вибір у меню планів: {choice}");
                    break;
            }
        }
    }
    private static void FilesMenu(Clinic clinic)
    {
        while (true)
        {
            Console.WriteLine("\n--- Файли ---");
            Console.WriteLine("1. Останні рядки лога");
            Console.WriteLine("2. Очистити лог");
            Console.WriteLine("3. Експортувати всі звіти");
            Console.WriteLine("4. Імпорт пацієнтів з CSV");
            Console.WriteLine("5. Назад");
            Console.Write("Оберіть дію: ");
            string choice = Console.ReadLine();
            if (choice == "5") break;
            switch (choice)
            {
                case "1":
                    {
                        Console.Write("Кількість рядків N: ");
                        if (int.TryParse(Console.ReadLine(), out int n))
                        {
                            string[] lines = clinic.Logger.GetLastLines(n);
                            if (lines.Length == 0)
                            {
                                Console.WriteLine("Лог порожній або файл не існує.");
                            }
                            else
                            {
                                for (int i = 0; i < lines.Length; i++)
                                {
                                    Console.WriteLine(lines[i]);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некоректне число.");
                            clinic.Logger.LogWarning("Помилка введення: некоректне число рядків лога.");
                        }
                    }
                    break;
                case "2":
                    {
                        clinic.Logger.Clear();
                        Console.WriteLine("Лог очищено.");
                    }
                    break;
                case "3":
                    {
                        string[] paths = clinic.Exporter.ExportAll();
                        Console.WriteLine("Звіти успішно експортовано:");
                        for (int i = 0; i < paths.Length; i++)
                        {
                            Console.WriteLine(paths[i]);
                        }
                        clinic.Logger.LogInfo("Експортовано всі текстові звіти у теку reports.");
                    }
                    break;
                case "4":
                    {
                        Console.Write("Шлях до CSV-файлу: ");
                        string path = Console.ReadLine();
                        CsvImporter importer = new CsvImporter();
                        ImportResult result = importer.ImportPatients(clinic, path);
                        result.Print();
                        clinic.Logger.LogInfo($"Імпорт CSV завершено: імпортовано {result.Imported}, пропущено {result.Skipped}.");
                    }
                    break;
                default:
                    Console.WriteLine("Некоректний варіант.");
                    break;
            }
        }
    }
}