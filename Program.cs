// === ТЕСТ ЗАДАЧІ 8: GrowablePatientManager ===
using ClinicApp;

Console.WriteLine("=== Тест GrowablePatientManager ===");
Console.WriteLine("Додаємо пацієнтів одного за одним ...");

GrowablePatientManager growableManager = new GrowablePatientManager();

for (int i = 1; i <= 20; i++)
{
    Patient p = new Patient($"Пацієнт{i}", $"Прізвище{i}");
    // Примусово встановимо ID для перевірки пошуку у тесті, якщо потрібно, або користуємось автоматичним
    growableManager.Add(p);
    Console.WriteLine($"Додано [{i}]. Розмір: {growableManager.Count} / {growableManager.Capacity}");
}

Console.WriteLine("\nТест пошуку:");
// Знайдемо пацієнта за ID 10 (якщо нумерація ID починається з 1)
Patient? found10 = growableManager.FindById(10);
if (found10 != null)
    Console.WriteLine($"FindById(10) -> {found10.FullName}");
else
    Console.WriteLine("FindById(10) -> не знайдено");

Patient? found99 = growableManager.FindById(99);
if (found99 != null)
    Console.WriteLine($"FindById(99) -> {found99.FullName}");
else
    Console.WriteLine("FindById(99) -> не знайдено");

Console.WriteLine("\nПорівняння:");
Console.WriteLine("PatientManager:          100 місць (фіксовано)");
Console.WriteLine($"GrowablePatientManager:  {growableManager.Capacity} місця (зросте при потребі)");
Console.WriteLine("==================================================\n");