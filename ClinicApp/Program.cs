using ClinicApp;

DoctorManager dm = new DoctorManager();

Doctor d1 = new Doctor("Олег", "Сидоренко", "Кардіологія", "LIC-001", "0441234567");
d1.WorkEndHour = 16;

Doctor d2 = new Doctor("Наталія", "Мороз", "Неврологии", "LIC-002", "0442345678");
d2.WorkStartHour = 9;
d2.WorkEndHour = 18;

Doctor d3 = new Doctor("Андрій", "Власенко", "Педіатрія", "LIC-003", "0443456789");

dm.Add(d1);
dm.Add(d2);
dm.Add(d3);

dm.DisplayAll();
Console.WriteLine();
dm.DisplayStats();
