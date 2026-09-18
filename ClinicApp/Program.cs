using ClinicApp;

PatientManager pm = new PatientManager();

pm.Add(new Patient("Іван", "Петренко", new DateTime(1985, 5, 12), "A+", "0501234567"));
pm.Add(new Patient("Олена", "Коваль", new DateTime(1993, 10, 25), "B-", "0672345678"));
pm.Add(new Patient("Максим", "Бойко", new DateTime(2010, 3, 14), "0+", "0933456789"));
pm.Add(new Patient("Марія", "Ткач", new DateTime(2000, 1, 1), "Невідомо", "0000000000"));

Console.WriteLine();
pm.DisplayAll();
Console.WriteLine();
pm.DisplayStats();
