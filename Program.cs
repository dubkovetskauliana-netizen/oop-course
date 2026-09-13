using System;

namespace ClinicApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PatientManager patientManager = new PatientManager();
            DoctorManager doctorManager = new DoctorManager();
            AppointmentManager appointmentManager = new AppointmentManager(patientManager, doctorManager);

            // Початкові дані
            doctorManager.Add(new Doctor("Олег", "Сидоренко", "Кардіологія", "LIC-001", "0441234567", 8, 16));
            doctorManager.Add(new Doctor("Наталія", "Мороз", "Неврологія", "LIC-002", "0442345678", 9, 18));
            doctorManager.Add(new Doctor("Андрій", "Власенко", "Педіатрія", "LIC-003", "0443456789", 8, 17));

            patientManager.Add(new Patient("Іван", "Петренко", new DateTime(1985, 5, 12), "A+", "0501234567"));
            patientManager.Add(new Patient("Олена", "Коваль", new DateTime(1993, 8, 20), "B-", "0672345678"));
            patientManager.Add(new Patient("Максим", "Бойко", new DateTime(2010, 2, 10), "O+", "0933456789"));

            // Демонстраційні записи з прикладу
            appointmentManager.Book(1, 1, new DateTime(2026, 5, 9, 10, 0, 0), 30);
            appointmentManager.Book(2, 2, new DateTime(2026, 5, 9, 11, 0, 0), 45);
            appointmentManager.Book(3, 3, new DateTime(2026, 5, 10, 9, 0, 0), 20);
            appointmentManager.Book(99, 1, new DateTime(2026, 5, 10, 10, 0, 0)); // Тест помилки неіснуючого пацієнта

            RunMainChoiceMenu(patientManager, doctorManager, appointmentManager);
        }

        static void RunMainChoiceMenu(PatientManager pManager, DoctorManager dManager, AppointmentManager aManager)
        {
            while (true)
            {
                Console.WriteLine("\n=== ГОЛОВНЕ МЕНЮ КЛІНІКИ ===");
                Console.WriteLine("1. Керування пацієнтами");
                Console.WriteLine("2. Керування лікарями");
                Console.WriteLine("3. Керування записами (Appointments)");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть розділ: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        RunPatientMenu(pManager);
                        break;
                    case "2":
                        RunDoctorMenu(dManager);
                        break;
                    case "3":
                        RunAppointmentMenu(aManager, pManager, dManager);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        static void RunAppointmentMenu(AppointmentManager manager, PatientManager pManager, DoctorManager dManager)
        {
            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ ЗАПИСІВ ---");
                Console.WriteLine("1. Показати всі майбутні записи");
                Console.WriteLine("2. Створити новий запис (Book)");
                Console.WriteLine("3. Скасувати запис (Cancel)");
                Console.WriteLine("4. Позначити запис як виконаний (Complete)");
                Console.WriteLine("5. Знайти записи пацієнта");
                Console.WriteLine("6. Знайти записи лікаря");
                Console.WriteLine("0. Назад у головне меню");
                Console.Write("Оберіть опцію: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Майбутні записи:");
                        manager.DisplayList(manager.GetUpcoming());
                        break;
                    case "2":
                        Console.WriteLine("Доступні пацієнти:");
                        pManager.DisplayAll();
                        Console.WriteLine("\nДоступні лікарі:");
                        dManager.DisplayAll();

                        Console.Write("\nВведіть ID пацієнта: ");
                        if (int.TryParse(Console.ReadLine(), out int pId))
                        {
                            Console.Write("Введіть ID лікаря: ");
                            if (int.TryParse(Console.ReadLine(), out int dId))
                            {
                                Console.Write("Введіть дату та час (формат: yyyy-MM-dd HH:mm): ");
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime dt))
                                {
                                    Console.Write("Введіть тривалість у хвилинах (за замовчуванням 30): ");
                                    string? durationInput = Console.ReadLine();
                                    int duration = int.TryParse(durationInput, out int dMinutes) ? dMinutes : 30;

                                    manager.Book(pId, dId, dt, duration);
                                }
                                else
                                {
                                    Console.WriteLine("Некоректний формат дати.");
                                }
                            }
                            else { Console.WriteLine("Некоректний ID лікаря."); }
                        }
                        else { Console.WriteLine("Некоректний ID пацієнта."); }
                        break;
                    case "3":
                        Console.Write("Введіть ID запису для скасування: ");
                        if (int.TryParse(Console.ReadLine(), out int cancelId))
                        {
                            Console.Write("Введіть причину скасування (опціонально): ");
                            string reason = Console.ReadLine() ?? "";
                            manager.Cancel(cancelId, reason);
                        }
                        else { Console.WriteLine("Некоректний ID."); }
                        break;
                    case "4":
                        Console.Write("Введіть ID запису для завершення: ");
                        if (int.TryParse(Console.ReadLine(), out int completeId))
                        {
                            manager.Complete(completeId);
                        }
                        else { Console.WriteLine("Некоректний ID."); }
                        break;
                    case "5":
                        Console.Write("Введіть ID пацієнта: ");
                        if (int.TryParse(Console.ReadLine(), out int patientId))
                        {
                            Console.WriteLine($"Записи пацієнта #{patientId}:");
                            manager.DisplayList(manager.GetByPatient(patientId));
                        }
                        else { Console.WriteLine("Некоректний ID."); }
                        break;
                    case "6":
                        Console.Write("Введіть ID лікаря: ");
                        if (int.TryParse(Console.ReadLine(), out int doctorId))
                        {
                            Console.WriteLine($"Записи лікаря #{doctorId}:");
                            manager.DisplayList(manager.GetByDoctor(doctorId));
                        }
                        else { Console.WriteLine("Некоректний ID."); }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        static void RunPatientMenu(PatientManager manager)
        {
            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ ПАЦІЄНТІВ ---");
                Console.WriteLine("1. Показати всіх");
                Console.WriteLine("2. Додати пацієнта");
                Console.WriteLine("3. Знайти за ім'ям/прізвищем");
                Console.WriteLine("4. Видалити за ID");
                Console.WriteLine("5. Статистика");
                Console.WriteLine("0. Назад у головне меню");
                Console.Write("Оберіть опцію: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": manager.DisplayAll(); break;
                    case "2": AddPatientMenu(manager); break;
                    case "3": SearchPatientMenu(manager); break;
                    case "4": RemovePatientMenu(manager); break;
                    case "5": manager.DisplayStats(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }

        static void RunDoctorMenu(DoctorManager manager)
        {
            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ ЛІКАРІВ ---");
                Console.WriteLine("1. Показати всіх");
                Console.WriteLine("2. Додати лікаря");
                Console.WriteLine("3. Знайти за спеціальністю");
                Console.WriteLine("4. Видалити за ID");
                Console.WriteLine("5. Статистика");
                Console.WriteLine("0. Назад у головне меню");
                Console.Write("Оберіть опцію: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": manager.DisplayAll(); break;
                    case "2": AddDoctorMenu(manager); break;
                    case "3": SearchDoctorMenu(manager); break;
                    case "4": RemoveDoctorMenu(manager); break;
                    case "5": manager.DisplayStats(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }

        static void AddPatientMenu(PatientManager manager)
        {
            Console.Write("Введіть ім'я: ");
            string fName = Console.ReadLine() ?? "Невідомо";
            Console.Write("Введіть прізвище: ");
            string lName = Console.ReadLine() ?? "Невідомо";
            manager.Add(new Patient(fName, lName));
        }

        static void SearchPatientMenu(PatientManager manager)
        {
            Console.Write("Введіть пошуковий запит (ім'я або прізвище): ");
            string query = Console.ReadLine() ?? "";
            var found = manager.FindByName(query);
            if (found.Length == 0) Console.WriteLine("Нікого не знайдено.");
            else foreach (var p in found) Console.WriteLine(p);
        }

        static void RemovePatientMenu(PatientManager manager)
        {
            Console.Write("Введіть ID пацієнта для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (manager.Remove(id)) Console.WriteLine("Видалено успішно.");
                else Console.WriteLine("Не знайдено.");
            }
            else Console.WriteLine("Некоректний ID.");
        }

        static void AddDoctorMenu(DoctorManager manager)
        {
            Console.Write("Введіть ім'я: ");
            string fName = Console.ReadLine() ?? "Невідомо";
            Console.Write("Введіть прізвище: ");
            string lName = Console.ReadLine() ?? "Невідомо";
            Console.Write("Введіть спеціальність: ");
            string spec = Console.ReadLine() ?? "Терапевт";

            manager.Add(new Doctor(fName, lName, spec));
        }

        static void SearchDoctorMenu(DoctorManager manager)
        {
            Console.Write("Введіть спеціальність для пошуку: ");
            string spec = Console.ReadLine() ?? "";
            var found = manager.FindBySpeciality(spec);
            if (found.Length == 0) Console.WriteLine("Лікарів такої спеціальності не знайдено.");
            else foreach (var d in found) Console.WriteLine(d);
        }

        static void RemoveDoctorMenu(DoctorManager manager)
        {
            Console.Write("Введіть ID лікаря для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (manager.Remove(id)) Console.WriteLine("Лікаря видалено.");
                else Console.WriteLine("Лікаря з таким ID не знайдено.");
            }
            else Console.WriteLine("Некоректний ID.");
        }
    }
}