using ClinicApp;

PatientManager pm = new PatientManager();
pm.Add(new Patient("Іван", "Петренко", new DateTime(1985, 5, 12), BloodType.APositive, "0501234567"));

DoctorManager dm = new DoctorManager();
dm.Add(new Doctor("Олег", "Сидоренко", Speciality.Cardiology));

AppointmentManager am = new AppointmentManager(pm, dm);
am.Book(1, 1, new DateTime(2027, 5, 10, 10, 0, 0), 30);

Console.WriteLine("\n=== Перевантаження FindBySpeciality ===");
Doctor[] enumVersion = dm.FindBySpeciality(Speciality.Cardiology);
Doctor[] stringVersion = dm.FindBySpeciality("кардіо");
Console.WriteLine("Знайдено лікарів (enum): " + enumVersion.Length);
Console.WriteLine("Знайдено лікарів (string): " + stringVersion.Length);

Console.WriteLine("\n=== Перевантаження GetByDate ===");
Appointment[] todayApp = am.GetByDate(2027, 5, 10);
Console.WriteLine("Записів на дату: " + todayApp.Length);

Console.WriteLine("\n=== Тест TryFindById (out) ===");
if (pm.TryFindById(1, out Patient patient))
{
    Console.WriteLine("Знайдено: " + patient.FullName);
}
else
{
    Console.WriteLine("Пацієнта не знайдено.");
}

Console.WriteLine("\n=== Тест операторів ?. та ?? ===");
string name = pm.FindById(99)?.FullName ?? "не знайдено";
Console.WriteLine("Пошук ID 99: " + name);
