using System;
using System.IO;
using System.Text;
using ClinicApp.Events;

namespace ClinicApp.Utils;

public class ClinicLogger
{
    private string logPath;

    public ClinicLogger(string logPath = "clinic.log")
    {
        this.logPath = logPath;
    }

    private void WriteLog(string level, string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string line = $"[{timestamp}] [{level}] {message}{Environment.NewLine}";
        File.AppendAllText(logPath, line, Encoding.UTF8);
    }

    public void LogInfo(string message)
    {
        WriteLog("INFO ", message);
    }

    public void LogWarning(string message)
    {
        WriteLog("WARN ", message);
    }

    public void LogError(string message)
    {
        WriteLog("ERROR", message);
    }

    public void OnAppointmentBooked(object? sender, AppointmentEventArgs e)
    {
        LogInfo($"Запис #{e.AppointmentId} створено: пацієнт #{e.PatientId} → лікар #{e.DoctorId}");
    }

    public void OnAppointmentCancelled(object? sender, AppointmentEventArgs e)
    {
        LogInfo($"Запис #{e.AppointmentId} скасовано");
    }

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e)
    {
        LogInfo($"Запис #{e.AppointmentId} завершено");
    }

    public void OnUrgentAppointmentBooked(object? sender, AppointmentEventArgs e)
    {
        string noteText = string.IsNullOrWhiteSpace(e.Notes) ? "без примітки" : e.Notes;
        LogWarning($"ТЕРМІНОВИЙ запис #{e.AppointmentId}: {noteText}");

        Directory.CreateDirectory("alerts");
        string alertFile = Path.Combine("alerts", $"urgent_{DateTime.Now:yyyy-MM-dd}.txt");
        string alertLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ТЕРМІНОВИЙ запис #{e.AppointmentId}: пацієнт #{e.PatientId} → лікар #{e.DoctorId} ({noteText}){Environment.NewLine}";
        File.AppendAllText(alertFile, alertLine, Encoding.UTF8);
    }

    public void OnPatientAdded(object? sender, PatientEventArgs e)
    {
        LogInfo($"Новий пацієнт #{e.PatientId}: {e.FullName}");
    }

    public void OnPaymentReceived(object? sender, PaymentEventArgs e)
    {
        LogInfo($"Оплата запису #{e.AppointmentId}: {e.Amount:F2} грн");
    }

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e)
    {
        LogInfo($"План лікування #{e.PlanId} завершено (пацієнт #{e.PatientId}, діагноз: {e.Diagnosis})");
    }

    public string[] GetLastLines(int n)
    {
        if (!Exists() || n <= 0)
        {
            return new string[0];
        }

        string[] allLines = File.ReadAllLines(logPath, Encoding.UTF8);
        int skip = allLines.Length - n;
        if (skip < 0)
        {
            skip = 0;
        }

        int count = allLines.Length - skip;
        string[] result = new string[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = allLines[skip + i];
        }

        return result;
    }

    public void Clear()
    {
        if (Exists())
        {
            File.Delete(logPath);
        }
    }

    public bool Exists()
    {
        return File.Exists(logPath);
    }
}
