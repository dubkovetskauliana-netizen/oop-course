using System;
using System.Globalization;
using System.IO;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class SessionManager
{
    private string sessionPath;

    public SessionManager(string sessionPath = "session.dat")
    {
        this.sessionPath = sessionPath;
    }

    public bool Exists()
    {
        return File.Exists(sessionPath);
    }

    public void Save(Clinic clinic)
    {
        Patient[] patients = clinic.Patients.GetAll();

        using (StreamWriter writer = new StreamWriter(sessionPath, false, Encoding.UTF8))
        {
            writer.WriteLine("[PATIENTS]");
            for (int i = 0; i < patients.Length; i++)
            {
                Patient p = patients[i];
                writer.WriteLine($"{p.FirstName},{p.LastName},{p.DateOfBirth:dd.MM.yyyy},{p.BloodType},{p.Phone}");
            }
            writer.WriteLine("[END]");
        }
    }

    public int Load(Clinic clinic)
    {
        if (!Exists())
        {
            return 0;
        }

        string[] lines = File.ReadAllLines(sessionPath, Encoding.UTF8);
        string currentSection = "";
        int addedCount = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.StartsWith("["))
            {
                currentSection = line;
                continue;
            }

            if (currentSection == "[PATIENTS]")
            {
                try
                {
                    string[] parts = line.Split(',');
                    if (parts.Length != 5)
                    {
                        continue;
                    }

                    string firstName = parts[0].Trim();
                    string lastName = parts[1].Trim();
                    DateTime dateOfBirth = DateTime.ParseExact(parts[2].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    BloodType bloodType = (BloodType)Enum.Parse(typeof(BloodType), parts[3].Trim());
                    string phone = parts[4].Trim();

                    Patient[] existing = clinic.Patients.GetAll();
                    bool alreadyExists = false;

                    for (int j = 0; j < existing.Length; j++)
                    {
                        if (existing[j].FirstName == firstName &&
                            existing[j].LastName == lastName &&
                            existing[j].DateOfBirth.Date == dateOfBirth.Date)
                        {
                            alreadyExists = true;
                            break;
                        }
                    }

                    if (!alreadyExists)
                    {
                        Patient patient = new Patient(firstName, lastName, dateOfBirth, bloodType, phone);
                        clinic.Patients.Add(patient);
                        addedCount++;
                    }
                }
                catch
                {
                }
            }
        }

        return addedCount;
    }
}
