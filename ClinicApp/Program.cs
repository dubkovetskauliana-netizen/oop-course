using ClinicApp;

PatientManager pm = new PatientManager();
pm.Add(new Patient("Іван", "Петренко", new DateTime(1985, 5, 12), "A+", "0501234567"));
pm.Add(new Patient("Олена", "Коваль", new DateTime(1993, 10, 25), "B-", "0672345678"));
pm.Add(new Patient("Максим", "Бойко", new DateTime(2010, 3, 14), "0+", "0933456789"));

DoctorManager dm = new DoctorManager();
dm.Add(new Doctor("Олег", "Сидоренко", "Кардіологія"));
dm.Add(new Doctor("Наталія", "Мороз", "Неврологія"));
dm.Add(new Doctor("Андрій", "Власенко", "Педіатрія"));

AppointmentManager am = new AppointmentManager(pm, dm);

Console.WriteLine();
am.Book(1, 1, new DateTime(2026, 5, 9, 10, 0, 0), 30);
am.Book(2, 2, new DateTime(2026, 5, 9, 11, 0, 0), 45);
am.Book(3, 3, new DateTime(2026, 5, 10, 9, 0, 0), 20);
am.Book(99, 1, new DateTime(2026, 5, 10, 11, 0, 0), 30);

Console.WriteLine("\nМайбутні записи:");
am.DisplayList(am.GetUpcoming());

Console.WriteLine();
am.Cancel(1, "Пацієнт не зміг прийти");

Console.WriteLine("\nЗаписи пацієнта #2:");
am.DisplayList(am.GetByPatient(2));
