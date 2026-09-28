using System;
using ClinicApp.Utils;

namespace ClinicApp.Models
{
    public abstract class MedicalRecord
    {
        private static int _nextId = 1;

        public int Id { get; }
        public int PatientId { get; }
        public int DoctorId { get; }
        public DateTime Date { get; }
        public string Notes { get; set; }

        protected MedicalRecord(int patientId, int doctorId, DateTime date)
        {
            ClinicValidator.ValidatePositive(patientId, nameof(patientId));
            ClinicValidator.ValidatePositive(doctorId, nameof(doctorId));

            PatientId = patientId;
            DoctorId = doctorId;
            Date = date;
            Notes = "";

            Id = _nextId++;
        }

        public abstract string GetSummary();

        public virtual string GetRecordType()
        {
            return "Медичний запис";
        }

        public virtual bool IsActive()
        {
            // Перевіряємо, чи запис не старший за 6 місяців
            return Date >= DateTime.Now.AddMonths(-6);
        }

        public override string ToString()
        {
            string formattedDate = Date.ToString("dd.MM.yyyy");
            string baseInfo = $"[{Id}] {GetRecordType()} | {formattedDate} | {GetSummary()}";

            if (!string.IsNullOrEmpty(Notes))
            {
                return $"{baseInfo} | {Notes}";
            }

            return baseInfo;
        }
    }
}
