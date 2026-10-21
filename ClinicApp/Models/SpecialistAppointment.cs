using System;

namespace ClinicApp.Models;

// Клас є закритим для унаслідування
public sealed class SpecialistAppointment : Appointment
{
    // Властивість лише для читання
    public string ConsultationTopic { get; }

    // Конструктор за специфікацією (тривалість за замовчуванням 45)
    public SpecialistAppointment(int patientId, int doctorId, DateTime scheduledAt, string topic = "", int durationMinutes = 45)
        : base(patientId, doctorId, scheduledAt, durationMinutes)
    {
        ConsultationTopic = topic;
    }

    // На 30% дорожче за базову ціну
    public override decimal GetCost()
    {
        return base.GetCost() * 1.3m;
    }

    public override string GetDescription()
    {
        return "Консультація спеціаліста: " + ConsultationTopic;
    }
}
