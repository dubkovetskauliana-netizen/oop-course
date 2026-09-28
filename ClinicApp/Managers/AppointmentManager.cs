using System;
using ClinicApp.Models;
using ClinicApp.Interfaces;

namespace ClinicApp.Managers
{
    public class AppointmentManager
    {
        private readonly Appointment[] _appointments = new Appointment[1000];
        private int _count = 0;

        public int Count
        {
            get { return _count; }
        }

        // Повертаємо порожній конструктор
        public AppointmentManager()
        {
        }

        // Повертаємо конструктор з двома аргументами, який викликається в Clinic.cs на рядку 18
        public AppointmentManager(object arg1, object arg2)
        {
        }

        public void Add(Appointment appointment)
        {
            if (appointment == null) return;

            if (_count >= 1000)
            {
                Console.WriteLine("Помилка: досягнуто ліміту записів.");
                return;
            }

            _appointments[_count] = appointment;
            _count++;
        }

        public Appointment FindById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].Id == id)
                {
                    return _appointments[i];
                }
            }
            return null;
        }

        // --- Метод з Лаби 05, який викликається в Clinic.cs (рядок 24) ---
        public Appointment[] GetByDate(DateTime date)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].ScheduledAt.Date == date.Date)
                {
                    matchCount++;
                }
            }

            Appointment[] result = new Appointment[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].ScheduledAt.Date == date.Date)
                {
                    result[index] = _appointments[i];
                    index++;
                }
            }
            return result;
        }

        // --- Метод з Лаби 05, який викликається в Clinic.cs (рядок 30) ---
        public Appointment[] GetUpcoming()
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].IsUpcoming)
                {
                    matchCount++;
                }
            }

            Appointment[] result = new Appointment[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].IsUpcoming)
                {
                    result[index] = _appointments[i];
                    index++;
                }
            }
            return result;
        }

        // --- Метод відображення списку, який викликається в Clinic.cs (рядок 25) ---
        public void DisplayList(Appointment[] items)
        {
            if (items == null || items.Length == 0)
            {
                Console.WriteLine("Записів не знайдено.");
                return;
            }
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    Console.WriteLine(items[i]);
                }
            }
        }

        // --- Задача 1 Лаби 07 ---
        public Appointment[] GetAll()
        {
            Appointment[] result = new Appointment[_count];
            for (int i = 0; i < _count; i++)
            {
                result[i] = _appointments[i];
            }
            return result;
        }

        // --- Задача 2 Лаби 07 ---
        public static int CancelAll(ICancellable[] items, string reason = "")
        {
            if (items == null) return 0;

            int cancelledCount = 0;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    if (items[i].Cancel(reason))
                    {
                        cancelledCount++;
                    }
                }
            }
            return cancelledCount;
        }
    }
}
