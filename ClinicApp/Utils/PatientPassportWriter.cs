using System;
using System.IO;
using System.Text;
using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class PatientPassportWriter
{
    private Clinic clinic;
    private string directory;

    public PatientPassportWriter(Clinic clinic, string directory = "patients")
    {
        this.clinic = clinic;
        this.directory = directory;
        Directory.CreateDirectory(this.directory);
    }

    public void OnPatientAdded(object? sender, PatientEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    private void WritePassport(int patientId)
    {
        Patient? patient = clinic.Patients.FindById(patientId);
        if (patient == null)
        {
            return;
        }

        string filePath = Path.Combine(directory, $"passport_{patientId}.txt");
        MedicalRecord[] records = clinic.MedicalRecords.GetByPatient(patientId);
        Appointment[] allAppointments = clinic.Appointments.GetAll();
        TreatmentPlan[] plans = clinic.TreatmentPlans.GetByPatient(patientId);
        decimal debt = clinic.Billing.GetPatientDebt(patientId);

        using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            writer.WriteLine($"=== Паспорт пацієнта #{patient.Id} ===");
            writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
            writer.WriteLine();

            writer.WriteLine($"Ім'я: {patient.FullName}");
            writer.WriteLine($"Дата народження: {patient.DateOfBirth:dd.MM.yyyy} ({ClinicFormatter.FormatAge(patient.Age)})");
            writer.WriteLine($"Група крові: {ClinicFormatter.FormatBloodType(patient.BloodType)}   Телефон: {ClinicFormatter.FormatPhone(patient.Phone)}");
            writer.WriteLine();

            writer.WriteLine("-- Діагнози --");
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] is Diagnosis diagnosis)
                {
                    writer.WriteLine(diagnosis.GetSummary());
                }
            }

            writer.WriteLine("-- Аналізи --");
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] is LabResult labResult)
                {
                    writer.WriteLine(labResult.GetSummary());
                }
            }

            writer.WriteLine("-- Рецепти --");
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] is Prescription prescription)
                {
                    writer.WriteLine(prescription.GetSummary());
                }
            }
            writer.WriteLine();

            writer.WriteLine("-- Записи --");
            for (int i = 0; i < allAppointments.Length; i++)
            {
                if (allAppointments[i] != null && allAppointments[i].PatientId == patientId)
                {
                    writer.WriteLine(allAppointments[i].ToString());
                }
            }
            writer.WriteLine();

            writer.WriteLine("-- Плани лікування --");
            for (int i = 0; i < plans.Length; i++)
            {
                TreatmentPlan p = plans[i];
                writer.WriteLine($"[{p.Id}] {p.Diagnosis} | {p.DurationDays} днів | {p.Status}");
            }
            writer.WriteLine();

            writer.WriteLine($"Заборгованість: {debt:F2} грн");
        }
    }
}
