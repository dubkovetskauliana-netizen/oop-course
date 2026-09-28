using System;
using System.Globalization;
using ClinicApp.Utils;

namespace ClinicApp.Models
{
    public class LabResult : MedicalRecord
    {
        private string _testName;
        private string _unit;
        private string _referenceRange;

        public string TestName
        {
            get { return _testName; }
            set
            {
                ClinicValidator.ValidateName(value, nameof(TestName));
                _testName = value;
            }
        }

        public double Value { get; set; }

        public string Unit
        {
            get { return _unit; }
            set
            {
                ClinicValidator.ValidateName(value, nameof(Unit));
                _unit = value;
            }
        }

        public string ReferenceRange
        {
            get { return _referenceRange; }
            set
            {
                ClinicValidator.ValidateName(value, nameof(ReferenceRange));
                _referenceRange = value;
            }
        }

        public bool IsNormal { get; set; }

        public LabResult(int patientId, int doctorId, DateTime date, string testName, double value, string unit, string referenceRange, bool isNormal)
            : base(patientId, doctorId, date)
        {
            TestName = testName;
            Value = value;
            Unit = unit;
            ReferenceRange = referenceRange;
            IsNormal = isNormal;
        }

        public override string GetSummary()
        {
            // Використовуємо InvariantCulture, щоб дріб виводився через крапку (наприклад, 6.2)
            string stringValue = Value.ToString("0.#", CultureInfo.InvariantCulture);
            string summary = $"{TestName}: {stringValue} {Unit} (норма: {ReferenceRange})";

            if (!IsNormal)
            {
                summary += " \u25b3 поза нормою";
            }

            return summary;
        }

        public override string GetRecordType()
        {
            return "Аналіз";
        }
    }
}
