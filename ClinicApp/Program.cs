using ClinicApp;

Clinic clinic = new Clinic("Медична Клініка");

clinic.Patients.Add(new Patient("Іван", "Петренко", new DateTime(1985, 5, 12), "A+", "0501234567"));
clinic.Patients.Add(new Patient("Олена", "Коваль", new DateTime(1993, 10, 25), "B-", "0672345678"));
clinic.Patients.Add(new Patient("Максим", "Бойко", new DateTime(2010, 3, 14), "0+", "0933456789"));
clinic.Patients.Add(new Patient("Марія", "Ткач", new DateTime(2000, 1, 1), "Невідомо", "0000000000"));

clinic.Doctors.Add(new Doctor("Олег", "Сидоренко", "Кардіологія"));
clinic.Doctors.Add(new Doctor("Наталія", "Мороз", "Неврологія"));
clinic.Doctors.Add(new Doctor("Андрій", "Власенко", "Педіатрія"));

Console.WriteLine();
clinic.Appointments.Book(1, 1, new DateTime(2027, 5, 9, 10, 0, 0), 30);
clinic.Appointments.Book(2, 2, new DateTime(2027, 5, 9, 11, 0, 0), 45);
clinic.Appointments.Book(3, 3, new DateTime(2027, 5, 10, 9, 0, 0), 20);

Console.WriteLine();
clinic.DisplaySchedule(new DateTime(2027, 5, 9));

Console.WriteLine();
clinic.GenerateReport();
