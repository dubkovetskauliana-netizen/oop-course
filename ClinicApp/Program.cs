using ClinicApp.Enums;
using ClinicApp.Models;

Console.WriteLine("=== Тест безпечної обробки винятків try/catch (Lab 05 Task 04) ===");

try
{
    Console.WriteLine("Спроба 1: Передаємо правильні дані...");
    Patient correctPatient = new Patient("Олена", "Коваль", new DateTime(1995, 8, 20), BloodType.BPositive, "0671112233");
    Console.WriteLine("Успішно створено: " + correctPatient.FullName);

    Console.WriteLine("\nСпроба 2: Свідомо передаємо некоректний номер телефону (9 цифр)...");
    Patient wrongPatient = new Patient("Максим", "Бойко", new DateTime(2005, 3, 15), BloodType.OPositive, "093123456");
}
catch (ArgumentOutOfRangeException e)
{
    Console.WriteLine("Помилка діапазону: " + e.Message);
}
catch (ArgumentException e)
{
    Console.WriteLine("Помилка валідації: " + e.Message);
}

Console.WriteLine("\nПрограма не впала аварійно і продовжує працювати!");
