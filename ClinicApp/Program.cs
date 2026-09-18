using ClinicApp;

Patient p = new Patient("Іван", "Петренко", new DateTime(1985, 5, 12), BloodType.APositive, "0501234567");
Doctor d = new Doctor("Олег", "Сидоренко", Speciality.Cardiology, "LIC-001", "0441234567");
Appointment a = new Appointment(p.Id, d.Id, new DateTime(2027, 5, 9, 10, 0, 0), 30);

Console.WriteLine(p.ToString());
Console.WriteLine(d.ToString());
Console.WriteLine(a.ToString());

Console.WriteLine("\nСкасовуємо запис:");
a.Cancel("Пацієнт передумав");
Console.WriteLine(a.ToString());
