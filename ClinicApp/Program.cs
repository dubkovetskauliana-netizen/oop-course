using ClinicApp.Enums;
using ClinicApp.Models;
using ClinicApp.Managers;

Console.WriteLine("=== Тест просторів імен (Lab 05 Task 01) ===");

PatientManager pm = new PatientManager();
pm.Add(new Patient("Іван", "Петренко", new DateTime(1985, 5, 12), BloodType.APositive, "0501234567"));

if (pm.TryFindById(1, out Patient patient))
{
    Console.WriteLine("Успішно знайдено пацієнта: " + patient.FullName);
}
