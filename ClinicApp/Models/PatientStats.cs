using System;

namespace ClinicApp.Models
{
    public class PatientStats : IComparable<PatientStats>
    {
        public int PatientId { get; }
        public string FullName { get; }
        public int VisitCount { get; }
        public decimal TotalSpent { get; }
        public DateTime LastVisitDate { get; }

        public PatientStats(int patientId, string fullName, int visitCount, decimal totalSpent, DateTime lastVisitDate)
        {
            PatientId = patientId;
            FullName = fullName;
            VisitCount = visitCount;
            TotalSpent = totalSpent;
            LastVisitDate = lastVisitDate;
        }

        public int CompareTo(PatientStats other)
        {
            if (other == null) return 1;

          
            return other.VisitCount.CompareTo(this.VisitCount);
        }

        public override string ToString()
        {
            string dateDisplay = LastVisitDate == DateTime.MinValue
                ? "-"
                : LastVisitDate.ToString("yyyy-MM-dd");

            return $"[{PatientId}] {FullName} | Візитів: {VisitCount} | Витрачено: {TotalSpent:F2} грн | Останній візит: {dateDisplay}";
        }
    }
}
