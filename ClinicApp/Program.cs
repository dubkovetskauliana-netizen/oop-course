using ClinicApp;

Console.WriteLine("=== Тест форматера ===");
Console.WriteLine(ClinicFormatter.FormatBloodType(BloodType.APositive));
Console.WriteLine(ClinicFormatter.FormatAge(1));
Console.WriteLine(ClinicFormatter.FormatAge(3));
Console.WriteLine(ClinicFormatter.FormatAge(11));
Console.WriteLine(ClinicFormatter.FormatAge(21));
Console.WriteLine(ClinicFormatter.FormatPhone("0501234567"));

Console.WriteLine("\n=== Тест індексаторів ===");
PatientManager pm = new PatientManager();
pm.Add(new Patient("Іван", "Петренко", new DateTime(1985, 5, 12), BloodType.APositive, "0501234567"));

Patient? first = pm[0];
if (first != null)
{
    Console.WriteLine("Знайдено через індексатор: " + first.ToString());
}
