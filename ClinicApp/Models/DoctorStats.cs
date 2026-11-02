using System;

namespace ClinicApp.Models
{
    public class DoctorStats : IComparable<DoctorStats>
    {
        public int DoctorId { get; }
        public string FullName { get; }
        public int AppointmentCount { get; }
        public decimal TotalRevenue { get; }
        public DateTime LastAppointmentDate { get; }

        public DoctorStats(int doctorId, string fullName, int appointmentCount, decimal totalRevenue, DateTime lastAppointmentDate)
        {
            DoctorId = doctorId;
            FullName = fullName;
            AppointmentCount = appointmentCount;
            TotalRevenue = totalRevenue;
            LastAppointmentDate = lastAppointmentDate;
        }

        public int CompareTo(DoctorStats other)
        {
            if (other == null) return 1;

            // Сортування за спаданням кількості прийомів:
            // порівнюємо інший об'єкт (other) з поточним (this)
            return other.AppointmentCount.CompareTo(this.AppointmentCount);
        }

        public override string ToString()
        {
            string dateStr = LastAppointmentDate == DateTime.MinValue
                ? "Немає прийомів"
                : LastAppointmentDate.ToString("yyyy-MM-dd");

            return $"ID: {DoctorId} | Ім'я: {FullName} | Прийоми: {AppointmentCount} | Виручка: {TotalRevenue:F2} грн | Останній прийом: {dateStr}";
        }
    }
}
