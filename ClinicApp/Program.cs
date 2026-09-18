using ClinicApp;

Console.WriteLine("=== Тест GrowablePatientManager ===");
Console.WriteLine("Додаємо пацієнтів одного за одним ...");

GrowablePatientManager gpm = new GrowablePatientManager();

for (int i = 1; i <= 20; i++)
{
    gpm.Add(new Patient("Тест", "Пацієнт" + i));
}

Console.WriteLine("\nТест пошуку:");
Patient? found1 = gpm.FindById(10);
if (found1 != null)
{
    Console.WriteLine("FindById(10) -> " + found1.FullName);
}
else
{
    Console.WriteLine("FindById(10) -> не знайдено");
}

Patient? found2 = gpm.FindById(99);
if (found2 != null)
{
    Console.WriteLine("FindById(99) -> " + found2.FullName);
}
else
{
    Console.WriteLine("FindById(99) -> не знайдено");
}

Console.WriteLine("\nПорівняння:");
Console.WriteLine("  PatientManager:       100 місць (фіксовано)");
Console.WriteLine("  GrowablePatientManager: " + gpm.Capacity + " місця (зросте при потребі)");
