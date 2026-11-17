using System;
using System.IO;
using System.Text;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class ClinicExporter
{
    private Clinic clinic;
    private string baseDir;

    public ClinicExporter(Clinic clinic, string baseDir = "reports")
    {
        this.clinic = clinic;
        this.baseDir = baseDir;
    }

    private string EnsureDateDirectory()
    {
        string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
        string fullDir = Path.Combine(baseDir, dateFolder);
        Directory.CreateDirectory(fullDir);
        return fullDir;
    }

    public string ExportPatients()
    {
        string dir = EnsureDateDirectory();
        string filePath = Path.Combine(dir, "patients.txt");
        Patient[] patients = clinic.Patients.GetAll();

        using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            writer.WriteLine("=== Пацієнти – звіт ===");
            writer.WriteLine("Згенеровано: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
            writer.WriteLine();

            for (int i = 0; i < patients.Length; i++)
            {
                writer.WriteLine($"{i + 1,3}. {patients[i]}");
            }

            writer.WriteLine();
            writer.WriteLine($"Всього: {patients.Length}");
        }

        return filePath;
    }

    public string ExportAppointments()
    {
        string dir = EnsureDateDirectory();
        string filePath = Path.Combine(dir, "appointments.txt");
        Appointment[] appointments = clinic.Appointments.GetAll();

        using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            writer.WriteLine("=== Записи на прийом – звіт ===");
            writer.WriteLine("Згенеровано: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
            writer.WriteLine();

            for (int i = 0; i < appointments.Length; i++)
            {
                writer.WriteLine($"{i + 1,3}. {appointments[i]}");
            }

            writer.WriteLine();
            writer.WriteLine($"Всього: {appointments.Length}");
        }

        return filePath;
    }

    public string ExportBilling()
    {
        string dir = EnsureDateDirectory();
        string filePath = Path.Combine(dir, "billing.txt");
        IPayable[] unpaid = clinic.Billing.GetAllUnpaid();
        decimal totalDebt = clinic.Billing.GetTotalDebt();

        using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            writer.WriteLine("=== Неоплачені рахунки – звіт ===");
            writer.WriteLine("Згенеровано: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
            writer.WriteLine();

            for (int i = 0; i < unpaid.Length; i++)
            {
                if (unpaid[i] is Appointment appointment)
                {
                    writer.WriteLine($"{i + 1,3}. {appointment}");
                }
                else
                {
                    writer.WriteLine($"{i + 1,3}. Рахунок | Сума: {unpaid[i].GetCost():F2} грн");
                }
            }

            writer.WriteLine();
            writer.WriteLine($"Всього: {unpaid.Length}");
            writer.WriteLine($"Загальний борг: {totalDebt:F2} грн");
        }

        return filePath;
    }

    public string ExportTreatmentPlans()
    {
        string dir = EnsureDateDirectory();
        string filePath = Path.Combine(dir, "treatment_plans.txt");
        TreatmentPlan[] plans = clinic.TreatmentPlans.GetAll();

        using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            writer.WriteLine("=== Плани лікування – звіт ===");
            writer.WriteLine("Згенеровано: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
            writer.WriteLine();

            for (int i = 0; i < plans.Length; i++)
            {
                TreatmentPlan p = plans[i];
                writer.WriteLine($"{i + 1,3}. [{p.Id}] Пацієнт #{p.PatientId} | Діагноз: {p.Diagnosis} | Тривалість: {p.DurationDays} дн. | Статус: {p.Status}");
            }

            writer.WriteLine();
            writer.WriteLine($"Всього: {plans.Length}");
        }

        return filePath;
    }

    public string[] ExportAll()
    {
        string[] paths = new string[4];
        paths[0] = ExportPatients();
        paths[1] = ExportAppointments();
        paths[2] = ExportBilling();
        paths[3] = ExportTreatmentPlans();
        return paths;
    }
}
