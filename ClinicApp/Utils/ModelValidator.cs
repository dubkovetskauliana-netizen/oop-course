using ClinicApp.Attributes;
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
            result.AddError("Object cannot be null.");
            return result;
        }

        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();

        foreach (PropertyInfo prop in properties)
        {
            object value = prop.GetValue(obj);
            string propName = prop.Name;

            var requiredAttr = prop.GetCustomAttribute<RequiredAttribute>();
            if (requiredAttr != null)
            {
                if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                {
                    result.AddError(string.IsNullOrEmpty(requiredAttr.ErrorMessage)
                        ? $"{propName}: Field is required."
                        : requiredAttr.ErrorMessage);
                }
            }

            var maxLengthAttr = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLengthAttr != null && value is string text)
            {
                if (text != null && text.Length > maxLengthAttr.Length)
                {
                    result.AddError(string.IsNullOrEmpty(maxLengthAttr.ErrorMessage)
                        ? $"{propName}: Max length exceeded."
                        : maxLengthAttr.ErrorMessage);
                }
            }

            var minValueAttr = prop.GetCustomAttribute<MinValueAttribute>();
            if (minValueAttr != null && value != null)
            {
                try
                {
                    double numValue = Convert.ToDouble(value);
                    if (numValue < minValueAttr.Min)
                    {
                        result.AddError(string.IsNullOrEmpty(minValueAttr.ErrorMessage)
                            ? $"{propName}: Value is too small."
                            : minValueAttr.ErrorMessage);
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

        Console.WriteLine($"Тип: {type.Name}");
        PropertyInfo[] properties = type.GetProperties();

        foreach (PropertyInfo prop in properties)
        {
            Console.Write($"  Властивість: {prop.PropertyType.Name} {prop.Name}");
            var attributes = prop.GetCustomAttributes();
            bool hasAttributes = false;

            foreach (var attr in attributes)
            {
                if (!hasAttributes)
                {
                    Console.Write(" [Атрибути: ");
                    hasAttributes = true;
                }
                string attrName = attr.GetType().Name;
                if (attrName.EndsWith("Attribute"))
                {
                    attrName = attrName.Substring(0, attrName.Length - "Attribute".Length);
                }
                Console.Write($"{attrName} ");
            }
            if (hasAttributes) Console.Write("]");
            Console.WriteLine();
        }
    }
}
