using ClinicApp.Enums;
using System;
using System.Reflection;

namespace ClinicApp.Utils;

public static class FormBuilder
{
    public static T Build<T>() where T : new()
    {
        T instance = new T();
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties();

        Console.WriteLine($"\nСтворення об'єкта {type.Name}:");

        foreach (PropertyInfo prop in properties)
        {
            if (!prop.CanWrite || prop.Name == "Id" || prop.Name == "Status") continue;

            while (true)
            {
                Console.Write($"{prop.Name}: ");
                string input = Console.ReadLine();

                try
                {
                    object convertedValue = ConvertValue(input, prop.PropertyType);
                    prop.SetValue(instance, convertedValue);
                    break;
                }
                catch
                {
                    Console.WriteLine("Некоректний формат введення. Спробуйте ще раз.");
                }
            }
        }

        ValidationResult validationResult = ModelValidator.Validate(instance);
        if (!validationResult.IsValid)
        {
            Console.WriteLine("\nПомилки валідації форми:");
            foreach (string error in validationResult.Errors)
            {
                Console.WriteLine($"- {error}");
            }
        }

        return instance;
    }

    private static object ConvertValue(string input, Type targetType)
    {
        if (targetType == typeof(string))
        {
            return input;
        }

        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        if (targetType.IsEnum)
        {
            return Enum.Parse(targetType, input, true);
        }

        Type underlyingType = Nullable.GetUnderlyingType(targetType);
        if (underlyingType != null)
        {
            return Convert.ChangeType(input, underlyingType);
        }

        return Convert.ChangeType(input, targetType);
    }
}
