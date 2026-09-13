using System;

namespace ClinicApp
{
    public class Appointment
    {
        private static int _idCounter = 1;

        public int Id { get; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateTime { get; set; }
        public int DurationMinutes { get; set; }
        public string Status { get; set; } // "Заплановано", "Скасовано", "Виконано"
        public string Notes { get; set; }

        public Appointment(int patientId, int doctorId, DateTime dateTime, int durationMinutes = 30)
        {
            Id = _idCounter++;
            PatientId = patientId;
            DoctorId = doctorId;
            DateTime = dateTime;
            DurationMinutes = durationMinutes;
            Status = "Заплановано";
            Notes = string.Empty;
        }

        public override string ToString()
        {
            return $"Запис #{Id} | Пацієнт ID: {PatientId} | Лікар ID: {DoctorId} | Дата: {DateTime:dd.MM.yyyy HH:mm} | Статус: {Status}";
        }
    }
}