using System;
using System.Globalization;
using System.IO;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class CsvImporter
{
    public ImportResult ImportPatients(Clinic clinic, string filePath)
    {
        ImportResult result = new ImportResult();

        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            result.AddError(0, "Файл не знайдено.");
            return result;
        }

        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            int lineNumber = i + 1;

            try
            {
                string[] parts = line.Split(',');
                if (parts.Length != 5)
                {
                    string word = "полів";
                    if (parts.Length == 1)
                    {
                        word = "поле";
                    }
                    else if (parts.Length >= 2 && parts.Length <= 4)
                    {
                        word = "поля";
                    }

                    result.AddError(lineNumber, $"Рядок має {parts.Length} {word} замість 5.");
                    continue;
                }

                string firstName = parts[0].Trim();
                string lastName = parts[1].Trim();
                DateTime dateOfBirth = DateTime.ParseExact(parts[2].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                BloodType bloodType = (BloodType)Enum.Parse(typeof(BloodType), parts[3].Trim());
                string phone = parts[4].Trim();

                Patient patient = new Patient(firstName, lastName, dateOfBirth, bloodType, phone);
                clinic.Patients.Add(patient);
                result.AddSuccess();
            }
            catch (Exception ex)
            {
                result.AddError(lineNumber, ex.Message);
            }
        }

        return result;
    }
}
