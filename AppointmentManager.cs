using System;

namespace ClinicApp
{
    public class AppointmentManager
    {
        private const int MaxAppointments = 500;
        private readonly Appointment[] _appointments;
        private int _count;

        private readonly PatientManager _patients;
        private readonly DoctorManager _doctors;

        public int Count => _count;

        public AppointmentManager(PatientManager patients, DoctorManager doctors)
        {
            _patients = patients;
            _doctors = doctors;
            _appointments = new Appointment[MaxAppointments];
            _count = 0;
        }

        private Appointment? FindById(int id)
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

        public bool Book(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
        {
            var patient = _patients.FindById(patientId);
            if (patient == null)
            {
                Console.WriteLine($"Помилка: пацієнта з ID {patientId} не знайдено.");
                return false;
            }

            var doctor = _doctors.FindById(doctorId);
            if (doctor == null)
            {
                Console.WriteLine($"Помилка: лікаря з ID {doctorId} не знайдено.");
                return false;
            }

            if (_count >= MaxAppointments)
            {
                Console.WriteLine("Помилка: досягнуто максимальну кількість записів.");
                return false;
            }

            Appointment newAppointment = new Appointment(patientId, doctorId, scheduledAt, durationMinutes);
            _appointments[_count++] = newAppointment;

            Console.Write("Запис ");
            DisplayAppointment(newAppointment);
            Console.WriteLine(" створено.");
            return true;
        }

        public bool Cancel(int id, string reason = "")
        {
            var appointment = FindById(id);
            if (appointment != null && appointment.Cancel(reason))
            {
                Console.WriteLine($"Запис [{id}] скасовано.");
                return true;
            }
            Console.WriteLine($"Помилка: запис із ID {id} не знайдено або він вже не є активним.");
            return false;
        }

        public bool Complete(int id)
        {
            var appointment = FindById(id);
            if (appointment != null && appointment.Complete())
            {
                Console.WriteLine($"Запис [{id}] позначено як виконаний.");
                return true;
            }
            Console.WriteLine($"Помилка: запис із ID {id} не знайдено.");
            return false;
        }

        public Appointment[] GetByPatient(int patientId)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].PatientId == patientId) matchCount++;
            }

            Appointment[] result = new Appointment[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].PatientId == patientId)
                {
                    result[index++] = _appointments[i];
                }
            }
            return result;
        }

        public Appointment[] GetByDoctor(int doctorId)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].DoctorId == doctorId) matchCount++;
            }

            Appointment[] result = new Appointment[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].DoctorId == doctorId)
                {
                    result[index++] = _appointments[i];
                }
            }
            return result;
        }

        public Appointment[] GetByDate(DateTime date)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].ScheduledAt.Date == date.Date) matchCount++;
            }

            Appointment[] result = new Appointment[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].ScheduledAt.Date == date.Date)
                {
                    result[index++] = _appointments[i];
                }
            }
            return result;
        }

        public Appointment[] GetUpcoming()
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].IsUpcoming) matchCount++;
            }

            Appointment[] result = new Appointment[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].IsUpcoming)
                {
                    result[index++] = _appointments[i];
                }
            }
            return result;
        }

        public void DisplayAppointment(Appointment app)
        {
            var patient = _patients.FindById(app.PatientId);
            var doctor = _doctors.FindById(app.DoctorId);

            string patientStr = patient != null ? patient.FullName : $"Пацієнт #{app.PatientId}";
            string doctorStr = doctor != null ? doctor.FullName : $"Лікар #{app.DoctorId}";

            Console.Write($"[{app.Id}] {patientStr} → {doctorStr} о {app.ScheduledAt:dd.MM.yyyy HH:mm}");
        }

        public void DisplayList(Appointment[] list)
        {
            if (list.Length == 0)
            {
                Console.WriteLine("Записів не знайдено.");
                return;
            }

            foreach (var app in list)
            {
                var patient = _patients.FindById(app.PatientId);
                var doctor = _doctors.FindById(app.DoctorId);

                string patientStr = patient != null ? patient.FullName : $"Пацієнт #{app.PatientId}";
                string doctorStr = doctor != null ? doctor.FullName : $"Лікар #{app.DoctorId}";

                string line = $"[{app.Id}] {patientStr} → {doctorStr} | {app.ScheduledAt:dd.MM.yyyy HH:mm}–{app.EndsAt:HH:mm} | {app.Status}";
                if (!string.IsNullOrEmpty(app.Notes))
                {
                    line += $" | {app.Notes}";
                }
                Console.WriteLine(line);
            }
        }
    }
}
