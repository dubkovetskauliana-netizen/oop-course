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

        public AppointmentManager()
        {
        }

        public AppointmentManager(object arg1, object arg2)
        {
        }

        // Приватний допоміжний метод, щоб не копіювати логіку перевірок і додавання
        private bool InternalAddAndCheck(Appointment appointment)
        {
            if (appointment == null) return false;

            if (_count >= 1000)
            {
                Console.WriteLine("Помилка: досягнуто ліміту записів.");
                return false;
            }

            _appointments[_count] = appointment;
            _count++;
            return true;
        }

        public void Add(Appointment appointment)
        {
            InternalAddAndCheck(appointment);
        }

        // Задача 3: Метод створює RegularAppointment замість звичайного Appointment
        public bool Book(int patientId, int doctorId, DateTime scheduledAt)
        {
            RegularAppointment appointment = new RegularAppointment(patientId, doctorId, scheduledAt);
            return InternalAddAndCheck(appointment);
        }

        // Задача 3: Метод для створення термінового прийому
        public bool BookUrgent(int patientId, int doctorId, DateTime scheduledAt, string urgencyNote = "", int durationMinutes = 30)
        {
            UrgentAppointment appointment = new UrgentAppointment(patientId, doctorId, scheduledAt, urgencyNote, durationMinutes);
            return InternalAddAndCheck(appointment);
        }

        // Задача 3: Метод для створення прийому спеціаліста
        public bool BookSpecialist(int patientId, int doctorId, DateTime scheduledAt, string topic = "", int durationMinutes = 45)
        {
            SpecialistAppointment appointment = new SpecialistAppointment(patientId, doctorId, scheduledAt, topic, durationMinutes);
            return InternalAddAndCheck(appointment);
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

        public Appointment[] GetAll()
        {
            Appointment[] result = new Appointment[_count];
            for (int i = 0; i < _count; i++)
            {
                result[i] = _appointments[i];
            }
            return result;
        }

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
