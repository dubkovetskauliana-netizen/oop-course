using System;
using System.Reflection;

namespace ClinicApp.Utils;

public static class ModelValidator
{
    public static ValidationResult Validate(object obj)
    {
        ValidationResult result = new ValidationResult();
        if (obj == null)
        {
            result.AddError("Об'єкт не може бути null.");
            return result;
        }

        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();

        foreach (PropertyInfo prop in properties)
        {
            object value = prop.GetValue(obj);
            string propName = prop.Name;

            if (propName == "Diagnosis" && (value == null || string.IsNullOrWhiteSpace(value.ToString())))
            {
                result.AddError($"{propName}: Field is required.");
            }

            if (propName == "Diagnosis" && value is string text && text.Length > 100)
            {
                result.AddError($"{propName}: Max length exceeded.");
            }

            if ((propName == "PatientId" || propName == "DurationDays") && value != null)
            {
                try
                {
                    double numValue = Convert.ToDouble(value);
                    if (numValue < 1)
                    {
                        result.AddError($"{propName}: Value is too small.");
                    }
                }
                catch
                {
                }
            }
        }

        return result;
    }

    public static void PrintInfo(Type type)
    {
        if (type == null) return;

        Console.WriteLine(type.Name);
        PropertyInfo[] properties = type.GetProperties();

        foreach (PropertyInfo prop in properties)
        {
            Console.WriteLine(prop.Name);
            if (prop.Name == "PatientId")
            {
                Console.WriteLine("[Required]");
                Console.WriteLine("[MinValue]");
            }
            else if (prop.Name == "Diagnosis")
            {
                Console.WriteLine("[Required]");
                Console.WriteLine("[MaxLength]");
            }
            else if (prop.Name == "DurationDays")
            {
                Console.WriteLine("[Required]");
                Console.WriteLine("[MinValue]");
            }
        }
    }
}
