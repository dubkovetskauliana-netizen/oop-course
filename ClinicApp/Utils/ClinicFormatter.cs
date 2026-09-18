using ClinicApp.Enums;

namespace ClinicApp.Utils;

public static class ClinicFormatter
{
    public static string FormatBloodType(BloodType bt)
    {
        switch (bt)
        {
            case BloodType.APositive: return "A+";
            case BloodType.ANegative: return "A-";
            case BloodType.BPositive: return "B+";
            case BloodType.BNegative: return "B-";
            case BloodType.ABPositive: return "AB+";
            case BloodType.ABNegative: return "AB-";
            case BloodType.OPositive: return "0+";
            case BloodType.ONegative: return "0-";
            default: return "Невідомо";
        }
    }

    public static string FormatSpeciality(Speciality s)
    {
        switch (s)
        {
            case Speciality.General: return "Загальна практика";
            case Speciality.Cardiology: return "Кардіологія";
            case Speciality.Neurology: return "Неврологія";
            case Speciality.Pediatrics: return "Педіатрія";
            case Speciality.Surgery: return "Хірургія";
            case Speciality.Orthopedics: return "Ортопедія";
            case Speciality.Dermatology: return "Дерматологія";
            case Speciality.Emergency: return "Невідкладна допомога";
            default: return "Загальна";
        }
    }

    public static string FormatAge(int age)
    {
        int remainder100 = age % 100;
        if (remainder100 >= 11 && remainder100 <= 19)
        {
            return age + " років";
        }

        int remainder10 = age % 10;
        if (remainder10 == 1)
        {
            return age + " рік";
        }
        if (remainder10 >= 2 && remainder10 <= 4)
        {
            return age + " роки";
        }
        return age + " років";
    }

    public static string FormatPhone(string phone)
    {
        if (phone.Length != 10)
        {
            return phone;
        }

        for (int i = 0; i < phone.Length; i++)
        {
            if (phone[i] < '0' || phone[i] > '9')
            {
                return phone;
            }
        }

        return " (" + phone.Substring(0, 3) + ") " + phone.Substring(3, 3) + "-" + phone.Substring(6);
    }
}
