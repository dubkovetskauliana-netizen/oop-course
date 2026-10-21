using System;
using System.Globalization;
using System.Threading;
using ClinicApp.Models;
using ClinicApp.Interfaces;
using ClinicApp.Managers;

namespace ClinicApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Clinic clinic = new Clinic();
            SeedData(clinic);

            // Демонстрація new проти override за специфікацією Задачі 3
            Console.WriteLine("\n== new vs override ==");

            UrgentAppointment urgentObj = new UrgentAppointment(4, 1, DateTime.Today.AddDays(2), "тест");
            Appointment appRef = urgentObj;
            UrgentAppointment urgentRef = urgentObj;

            Console.WriteLine("Через Appointment:      " + appRef.GetDescription() + " | пріоритет " + appRef.GetPriority());
            Console.WriteLine("Через UrgentAppointment: " + urgentRef.GetDescription() + " | пріоритет " + urgentRef.GetPriority());

            /*
            ВІДПОВІДІ НА ЗАПИТАННЯ З КУРСУ:
            1. Чому GetDescription() через змінну типу Appointment дає опис термінового прийому?
               - Метод GetDescription() перевизначений через 'override'. При виклику віртуальних методів 
                 середовище C# завжди орієнтується на реальний тип об'єкта в пам'яті (UrgentAppointment).

            2. Чому GetPriority() через таку саму змінну повертає 3, хоча об'єкт — UrgentAppointment?
               - Метод GetPriority() прихований за допомогою 'new'. Приховування методів не є поліморфним. 
                 Який метод виконається — визначається виключно типом змінної (для Appointment це базовий метод, що повертає 3).

            3. Що треба змінити в Appointment і UrgentAppointment, щоб GetPriority() теж працював поліморфно?
               - У базовому класі Appointment перед методом GetPriority() треба дописати 'virtual', 
                 а в підкласі UrgentAppointment замість модифікатора 'new' використати 'override'.
            */

            Console.WriteLine("\nНатисніть Enter для переходу до головного меню...");
            Console.ReadLine();

            while (true)
            {
                Console.WriteLine("================================================");
                Console.WriteLine("||              МЕДИЧНА КЛІНІКА               ||");
                Console.WriteLine("================================================");
                Console.WriteLine("|| 1. Пацієнти     – реєстрація, пошук        ||");
                Console.WriteLine("|| 2. Лікарі       – персонал, розклад        ||");
                Console.WriteLine("|| 3. Записи       – прийоми, скасування      ||");
                Console.WriteLine("|| 4. Медична картка – діагнози, рецепти      ||");
                Console.WriteLine("|| 5. Рахунки      – оплата, борги            ||");
                Console.WriteLine("|| 6. Звіт         – загальна статистика      ||");
                Console.WriteLine("|| 0. Вийти                                   ||");
                Console.WriteLine("================================================");
                Console.Write("Оберіть розділ: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "2":
                        DoctorsMenu(clinic);
                        break;
                    case "3":
                        AppointmentsMenu(clinic);
                        break;
                    case "5":
                        BillingMenu(clinic);
                        break;
                    case "1":
                    case "4":
                    case "6":
                        Console.WriteLine("Розділ працює у фоновому режимі з минулих лаб.");
                        break;
                    default:
                        Console.WriteLine("Некоректний вибір.");
                        break;
                }
            }
        }

        private static void SeedData(Clinic clinic)
        {
            clinic.Appointments.Book(1, 1, DateTime.Today.AddHours(9));
            clinic.Appointments.BookUrgent(2, 2, DateTime.Today.AddHours(11), "Біль у грудях");
            clinic.Appointments.BookSpecialist(3, 3, DateTime.Today.AddHours(12), "кардіологія", 60);
        }

        private static void BillingMenu(Clinic clinic)
        {
            while (true)
            {
                Console.WriteLine("\n--- Рахунки ---");
                Console.WriteLine("1. Борги пацієнта");
                Console.WriteLine("2. Всі неоплачені записи");
                Console.WriteLine("3. Оплатити запис");
                Console.WriteLine("4. Загальний борг клініки");
                Console.WriteLine("0. Назад");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                        {
                            Console.Write("ID пацієнта (ціле): ");
                            if (!int.TryParse(Console.ReadLine(), out int pId)) break;

                            decimal debt = clinic.Billing.GetPatientDebt(pId);
                            IPayable[] unpaidItems = clinic.Billing.GetUnpaidByPatient(pId);

                            Console.WriteLine($"Неоплачені записи пацієнта #{pId}:");
                            clinic.Billing.DisplayUnpaid(unpaidItems);
                            Console.WriteLine($"Борг: {debt.ToString("F2")} грн");
                        }
                        break;

                    case "2":
                        {
                            IPayable[] allUnpaid = clinic.Billing.GetAllUnpaid();
                            clinic.Billing.DisplayUnpaid(allUnpaid);
                        }
                        break;

                    case "3":
                        {
                            Console.Write("ID запису для оплати: ");
                            if (!int.TryParse(Console.ReadLine(), out int appId)) break;

                            if (clinic.Billing.PayAppointment(appId))
                            {
                                Console.WriteLine($"Запис [{appId}] оплачено.");
                            }
                            else
                            {
                                Console.WriteLine("Не вдалося оплатити: запис не знайдено, вже оплачено або скасовано.");
                            }
                        }
                        break;

                    case "4":
                        {
                            decimal totalDebt = clinic.Billing.GetTotalDebt();
                            Console.WriteLine($"Загальний борг по клініці: {totalDebt.ToString("F2")} грн");
                        }
                        break;
                }
            }
        }

        private static void AppointmentsMenu(Clinic clinic)
        {
            while (true)
            {
                Console.WriteLine("\n--- Керування записами ---");
                Console.WriteLine("1. Переглянути всі прийоми");
                Console.WriteLine("8. Скасувати всі записи пацієнта");
                Console.WriteLine("9. За типом прийому");
                Console.WriteLine("0. Назад");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                if (choice == "1")
                {
                    clinic.Appointments.DisplayList(clinic.Appointments.GetAll());
                }
                else if (choice == "9")
                {
                    Console.WriteLine("Оберіть:");
                    Console.WriteLine("1. Термінові");
                    Console.WriteLine("2. Консультації спеціаліста");
                    Console.WriteLine("3. Звичайні");
                    Console.Write("Оберіть: ");

                    string typeChoice = Console.ReadLine();
                    Appointment[] filtered = null;

                    if (typeChoice == "1")
                    {
                        filtered = clinic.Appointments.GetUrgent();
                    }
                    else if (typeChoice == "2")
                    {
                        filtered = clinic.Appointments.GetSpecialist();
                    }
                    else if (typeChoice == "3")
                    {
                        filtered = clinic.Appointments.GetRegular();
                    }

                    if (filtered != null)
                    {
                        clinic.Appointments.DisplayList(filtered);
                    }
                    else
                    {
                        Console.WriteLine("Некоректний вибір типу.");
                    }
                }
                else if (choice == "8")
                {
                    Console.Write("ID пацієнта (ціле): ");
                    if (!int.TryParse(Console.ReadLine(), out int pId)) break;

                    Console.Write("Причина (Enter – без причини): ");
                    string reason = Console.ReadLine();

                    Appointment[] patientApps = clinic.Appointments.GetAll();

                    int count = 0;
                    for (int i = 0; i < patientApps.Length; i++)
                    {
                        if (patientApps[i] != null && patientApps[i].PatientId == pId) count++;
                    }

                    ICancellable[] cancellables = new ICancellable[count];
                    int idx = 0;
                    for (int i = 0; i < patientApps.Length; i++)
                    {
                        if (patientApps[i] != null && patientApps[i].PatientId == pId)
                        {
                            cancellables[idx] = (ICancellable)patientApps[i];
                            idx++;
                        }
                    }
                    int cancelled = AppointmentManager.CancelAll(cancellables, reason);
                    Console.WriteLine($"Скасовано записів: {cancelled}");
                }
            }
        }
        private static void DoctorsMenu(Clinic clinic)
        {
            while (true)
            {
                Console.WriteLine("\n--- Керування персоналом ---");
                Console.WriteLine("1. Список лікарів");
                Console.WriteLine("5. Вільні години лікаря");
                Console.WriteLine("0. Назад");
                Console.Write("Оберіть дію: ");
                string choice = Console.ReadLine();
                if (choice == "0") break;
                if (choice == "1")
                {
                    clinic.Doctors.DisplayAll();
                }
                else if (choice == "5")
                {
                    Console.Write("ID лікаря: ");
                    if (!int.TryParse(Console.ReadLine(), out int dId)) break;
                }
            }
        }
    }
}