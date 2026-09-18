using ClinicApp.Enums;
using ClinicApp.Models;

Console.WriteLine("=== Тест регулярних виразів Regex (Lab 05 Task 05) ===");

try
{
    Console.WriteLine("Спроба 1: Створення пацієнта з довгим правильним номером (+38)...");
    Patient p1 = new Patient("Олена", "Коваль", new DateTime(1995, 8, 20), BloodType.BPositive, "+380671112233");
    Console.WriteLine("Успішно: " + p1.ToString());

    Console.WriteLine("\nСпроба 2: Свідомо передаємо неправильний формат email...");
    p1.Email = "invalid-email-format";
}
catch (ArgumentException e)
{
    Console.WriteLine("Помилка валідації Regex: " + e.Message);
}
