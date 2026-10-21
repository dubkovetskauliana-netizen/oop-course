using System;
using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Appointment : ClinicApp.Interfaces.IIdentifiable

{
    private static int _nextId = 1;

    private int _durationMinutes;

    public int Id { get; }

    public int PatientId { get; }

    public int DoctorId { get; }

    public DateTime ScheduledAt { get; set; }

    public AppointmentStatus Status { get; set; }

    public string Notes { get; set; }

    // Задача 5: Множник знижки (за замовчуванням 1.0m, тобто без знижки)
    public decimal DiscountMultiplier { get; set; } = 1.0m;

    public int DurationMinutes
    {
        get => _durationMinutes;
        set { ClinicValidator.ValidatePositive(value, nameof(DurationMinutes)); _durationMinutes = value; }
    }

    public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);

    public bool IsUpcoming => ScheduledAt > DateTime.Now && Status == AppointmentStatus.Scheduled;

    public Appointment(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
    {
        Id = _nextId++;
        PatientId = patientId;
        DoctorId = doctorId;
        ScheduledAt = scheduledAt;
        DurationMinutes = durationMinutes;
        Status = AppointmentStatus.Scheduled;
        Notes = "";
    }

    public bool Cancel(string reason = "")
    {
        if (Status == AppointmentStatus.Scheduled)
        {
            Status = AppointmentStatus.Cancelled;
            Notes = reason;
            return true;
        }
        return false;
    }

    public bool Complete()
    {
        if (Status == AppointmentStatus.Scheduled)
        {
            Status = AppointmentStatus.Completed;
            return true;
        }
        return false;
    }

    // Задача 5: Враховуємо множник знижки у базовій вартості
    public virtual decimal GetCost()
    {
        return (DurationMinutes * 10) * DiscountMultiplier;
    }

    public virtual string GetDescription()
    {
        return "Прийом";
    }

    public int GetPriority()
    {
        return 3;
    }

    public override string ToString()
    {
        string result = "[" + Id + "] " + GetDescription() + " | Пацієнт #" + PatientId + " | Лікар #" + DoctorId + " | " + ScheduledAt.ToString("dd.MM.yyyy HH:mm") + " | " + Status + " | " + GetCost().ToString("F2") + " грн";

        if (Notes.Length > 0)
            result += " | " + Notes;

        return result;
    }
}
