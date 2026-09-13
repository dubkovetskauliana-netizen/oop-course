using System;

namespace ClinicApp
{
    public class Appointment
    {
        private static int _idCounter = 1;

        public int Id { get; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public int DurationMinutes { get; set; }
        public string Status { get; set; } = "Заплановано";
        public string Notes { get; set; } = string.Empty;

        public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);

        public bool IsUpcoming => Status == "Заплановано" && ScheduledAt > DateTime.Now;

        public Appointment(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
        {
            Id = _idCounter++;
            PatientId = patientId;
            DoctorId = doctorId;
            ScheduledAt = scheduledAt;
            DurationMinutes = durationMinutes;
        }

        public bool Cancel(string reason = "")
        {
            if (Status != "Заплановано") return false;
            Status = "Скасовано";
            if (!string.IsNullOrEmpty(reason))
            {
                Notes = reason;
            }
            return true;
        }

        public bool Complete()
        {
            if (Status != "Заплановано") return false;
            Status = "Виконано";
            return true;
        }
    }
}