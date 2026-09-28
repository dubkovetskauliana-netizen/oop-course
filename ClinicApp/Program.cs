using System;
using System.Globalization;
using System.Threading;
using ClinicApp.Models;

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
                Console.WriteLine("\n=== ГОЛОВНЕ МЕНЮ ===");
                Console.WriteLine("1. Керування пацієнтами");
                Console.WriteLine("2. Керування лікарями");
                Console.WriteLine("3. Розклад та записи");
                Console.WriteLine("4. Медична картка (Лаба 06)");
                Console.WriteLine("5. Звіт");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть пункт: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "4":
                        MedicalRecordsMenu(clinic);
                        break;
                    case "1":
                    case "2":
                    case "3":
                    case "5":
                        Console.WriteLine("Цей розділ працює у фоновому режимі з минулих лаб.");
                        break;
                    default:
                        Console.WriteLine("Некоректний вибір.");
                        break;
                }
            }
        }

        private static void SeedData(Clinic clinic)
        {
            DateTime today = DateTime.Today;

            // Наповнюємо менеджер медичних записів тестовими даними без створення нових пацієнтів,
            // щоб не виникало конфліктів із конструкторами минулих лабораторних.
            clinic.MedicalRecords.Add(new Diagnosis(1, 1, today.AddDays(-30), "I10", "Гіпертонічна хвороба", true));
            clinic.MedicalRecords.Add(new Diagnosis(1, 1, today.AddDays(-5), "J06.9", "Гострий ринофарингіт", false));
            clinic.MedicalRecords.Add(new LabResult(1, 1, today, "Гемоглобін", 145.0, "г/л", "120–160", true));
            clinic.MedicalRecords.Add(new LabResult(1, 2, today, "Холестерин", 6.2, "ммоль/л", "< 5.2", false));
            clinic.MedicalRecords.Add(new Prescription(1, 2, today.AddDays(-5), "Лізиноприл", "10 мг", 30, "1 раз на добу вранці"));
            clinic.MedicalRecords.Add(new Prescription(2, 1, today.AddDays(-40), "Аспірин", "100 мг", 10, "Після їжі"));
            clinic.MedicalRecords.Add(new Diagnosis(2, 2, today.AddDays(-10), "I25", "Ішемічна хвороба серця", true));
            clinic.MedicalRecords.Add(new LabResult(3, 1, today.AddDays(-2), "Глюкоза", 5.5, "ммоль/л", "3.3–5.5", true));
        }

        private static void MedicalRecordsMenu(Clinic clinic)
        {
            while (true)
            {
                Console.WriteLine("\n--- МЕДИЧНА КАРТКА ---");
                Console.WriteLine("1. Картка пацієнта (зведення)");
                Console.WriteLine("2. Усі записи пацієнта (демонстрація поліморфізму)");
                Console.WriteLine("3. Додати діагноз");
                Console.WriteLine("4. Додати аналіз");
                Console.WriteLine("5. Додати рецепт");
                Console.WriteLine("6. Записи лікаря");
                Console.WriteLine("0. Назад");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                        if (HeaderAndIdRequest(out int pId1)) clinic.MedicalRecords.DisplayPatientSummary(pId1);
                        break;
                    case "2":
                        if (HeaderAndIdRequest(out int pId2)) clinic.MedicalRecords.DisplayList(clinic.MedicalRecords.GetByPatient(pId2));
                        break;
                    case "3":
                        ExecuteAddDiagnosis(clinic);
                        break;
                    case "4":
                        ExecuteAddLabResult(clinic);
                        break;
                    case "5":
                        ExecuteAddPrescription(clinic);
                        break;
                    case "6":
                        Console.Write("ID лікаря: ");
                        if (int.TryParse(Console.ReadLine(), out int dId)) clinic.MedicalRecords.DisplayList(clinic.MedicalRecords.GetByDoctor(dId));
                        break;
                }
            }
        }

        private static void ExecuteAddDiagnosis(Clinic clinic)
        {
            if (!HeaderAndIdRequest(out int pId)) return;
            Console.Write("ID лікаря (ціле число): ");
            if (!int.TryParse(Console.ReadLine(), out int dId)) return;
            Console.Write("Код діагнозу (напр. I10): ");
            string code = Console.ReadLine();
            Console.Write("Опис: ");
            string desc = Console.ReadLine();
            Console.Write("Хронічне? (1=так, 0=ні): ");
            bool isChronic = Console.ReadLine() == "1";

            try
            {
                clinic.MedicalRecords.Add(new Diagnosis(pId, dId, DateTime.Today, code, desc, isChronic));
            }
            catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
        }

        private static void ExecuteAddLabResult(Clinic clinic)
        {
            if (!HeaderAndIdRequest(out int pId)) return;
            Console.Write("ID лікаря (ціле число): ");
            if (!int.TryParse(Console.ReadLine(), out int dId)) return;
            Console.Write("Назва аналізу: ");
            string name = Console.ReadLine();
            Console.Write("Значення (число через крапку): ");
            if (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out double val)) return;
            Console.Write("Одиниці виміру (напр. г/л): ");
            string unit = Console.ReadLine();
            Console.Write("Нормальний діапазон (текст): ");
            string range = Console.ReadLine();
            Console.Write("В межах норми? (1=так, 0=ні): ");
            bool isNormal = Console.ReadLine() == "1";

            try
            {
                clinic.MedicalRecords.Add(new LabResult(pId, dId, DateTime.Today, name, val, unit, range, isNormal));
            }
            catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
        }

        private static void ExecuteAddPrescription(Clinic clinic)
        {
            if (!HeaderAndIdRequest(out int pId)) return;
            Console.Write("ID лікаря (ціле число): ");
            if (!int.TryParse(Console.ReadLine(), out int dId)) return;
            Console.Write("Назва препарату: ");
            string med = Console.ReadLine();
            Console.Write("Дозування (напр. 10 мг): ");
            string dose = Console.ReadLine();
            Console.Write("Тривалість у днях (ціле число): ");
            if (!int.TryParse(Console.ReadLine(), out int days)) return;
            Console.Write("Інструкція з прийому: ");
            string inst = Console.ReadLine();

            try
            {
                clinic.MedicalRecords.Add(new Prescription(pId, dId, DateTime.Today, med, dose, days, inst));
            }
            catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
        }

        private static bool HeaderAndIdRequest(out int id)
        {
            Console.Write("ID пацієнта (ціле число): ");
            return int.TryParse(Console.ReadLine(), out id);
        }
    }
}
