using System.Text.RegularExpressions;

namespace ClinicApp.Utils;

public static class ClinicValidator
{
    private static readonly Regex _phoneRegex = new Regex(@"^(\+38)?\d{10}$");
    private static readonly Regex _emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    public static void ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(fieldName + " не може бути порожнім.");
        }
        if (value.Length > 50)
        {
            throw new ArgumentException(fieldName + " занадто довге (макс. 50 символів).");
        }
    }

    public static void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new ArgumentException("Телефон не може бути порожнім.");
        }
        if (!_phoneRegex.IsMatch(phone))
        {
            throw new ArgumentException("Некоректний формат телефону.");
        }
    }

    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email не може бути порожнім.");
        }
        if (!_emailRegex.IsMatch(email))
        {
            throw new ArgumentException("Некоректний формат email.");
        }
    }

    public static void ValidateDate(DateTime value, string fieldName)
    {
        if (value > DateTime.Today)
        {
            throw new ArgumentOutOfRangeException(fieldName, "Дата не може бути в майбутньому.");
        }
        if (value.Year < 1900)
        {
            throw new ArgumentOutOfRangeException(fieldName, "Дата не може бути раніше 1900 року.");
        }
    }

    public static void ValidatePositive(int value, string fieldName)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(fieldName, fieldName + " має бути більшим за 0.");
        }
    }
}
