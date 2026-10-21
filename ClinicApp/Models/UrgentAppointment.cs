using System;

namespace ClinicApp.Models;

public class UrgentAppointment : Appointment
{
    // Властивість лише для читання
    public string UrgencyNote { get; }

    // Конструктор з необов'язковими параметрами за замовчуванням
    public UrgentAppointment(int patientId, int doctorId, DateTime scheduledAt, string urgencyNote = "", int durationMinutes = 30)
        : base(patientId, doctorId, scheduledAt, durationMinutes)
    {
        UrgencyNote = urgencyNote;
    }

    // На 50% дорожче (множимо на 1.5m, викликаючи базовий GetCost)
    public override decimal GetCost()
    {
        return base.GetCost() * 1.5m;
    }

    // Закритий для подальшого перевизначення метод
    public sealed override string GetDescription()
    {
        if (string.IsNullOrWhiteSpace(UrgencyNote))
        {
            return "Терміновий";
        }
        return "Терміновий (" + UrgencyNote + ")";
    }

    // Навмисне приховування методу за допомогою new
    public new int GetPriority()
    {
        return 1;
    }
}
