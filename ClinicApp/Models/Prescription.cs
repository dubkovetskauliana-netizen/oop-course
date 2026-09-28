using System;
using ClinicApp.Utils;

namespace ClinicApp.Models
{
    public class Prescription : MedicalRecord
    {
        private string _medicationName;
        private string _dosage;
        private int _durationDays;

        public string MedicationName
        {
            get { return _medicationName; }
            set
            {
                ClinicValidator.ValidateName(value, nameof(MedicationName));
                _medicationName = value;
            }
        }

        public string Dosage
        {
            get { return _dosage; }
            set
            {
                ClinicValidator.ValidateName(value, nameof(Dosage));
                _dosage = value;
            }
        }

        public int DurationDays
        {
            get { return _durationDays; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(DurationDays), "Тривалість курсу має бути більшою за 0");
                }
                _durationDays = value;
            }
        }

        public string Instructions { get; set; }

        public DateTime ExpiresAt
        {
            get { return Date.AddDays(DurationDays); }
        }

        public Prescription(int patientId, int doctorId, DateTime date, string medicationName, string dosage, int durationDays, string instructions = "")
            : base(patientId, doctorId, date)
        {
            MedicationName = medicationName;
            Dosage = dosage;
            DurationDays = durationDays;
            Instructions = instructions;
        }

        public override string GetSummary()
        {
            string summary = $"{MedicationName} {Dosage} х {DurationDays} днів";
            if (!string.IsNullOrEmpty(Instructions))
            {
                summary += $" ({Instructions})";
            }
            return summary;
        }

        public override string GetRecordType()
        {
            return "Рецепт";
        }

        public override bool IsActive()
        {
            return ExpiresAt >= DateTime.Today;
        }
    }
}
