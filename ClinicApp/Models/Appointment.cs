using System;
using ClinicApp.Enums;
using ClinicApp.Utils;
using ClinicApp.Interfaces;

namespace ClinicApp.Models;

public class Appointment : IPayable, ICancellable
{
    private static int _nextId = 1;

    private int _durationMinutes;
    private bool _isPaid = false;

    public int Id { get; }
    public int PatientId { get; }
    public int DoctorId { get; }
    public DateTime ScheduledAt { get; set; }
    public AppointmentStatus Status { get; private set; }
    public string Notes { get; private set; }

    public int DurationMinutes
    {
        get => _durationMinutes;
        set { ClinicValidator.ValidatePositive(value, nameof(DurationMinutes)); _durationMinutes = value; }
    }

    public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);
    public bool IsUpcoming => ScheduledAt > DateTime.Now && Status == AppointmentStatus.Scheduled;

    // --- Реалізація інтерфейсу ICancellable ---
    public bool IsCancelled => Status == AppointmentStatus.Cancelled;

    public string CancellationReason => Status == AppointmentStatus.Cancelled ? Notes : "";

    // --- Реалізація інтерфейсу IPayable ---
    public decimal GetCost()
    {
        return (decimal)DurationMinutes * 10m;
    }

    public bool IsPaid => _isPaid;

    public void MarkPaid()
    {
        // Безпечний рефакторинг із Задачі 2: спираємось на нову властивість IsCancelled
        if (IsCancelled)
        {
            return;
        }
        _isPaid = true;
    }

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

    public override string ToString()
    {
        string result = "[" + Id + "] Пацієнт #" + PatientId + " -> Лікар #" + DoctorId + " | " + ScheduledAt.ToString("dd.MM.yyyy HH:mm") + "-" + EndsAt.ToString("HH:mm") + " | " + Status;
        if (Notes.Length > 0) result += " | " + Notes;
        return result;
    }
}
