using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Doctor
{
    private static int _nextId = 1;

    private string _firstName = "";
    private string _lastName = "";
    private string _licenseNumber = "";
    private string _phone = "";

    public int Id { get; }

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Ім'я не може бути порожнім.");
            }
            if (value.Length > 50)
            {
                throw new ArgumentException("Ім'я занадто довге (макс. 50 символів).");
            }
            _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Прізвище не може бути порожнім.");
            }
            if (value.Length > 50)
            {
                throw new ArgumentException("Прізвище занадто довге (макс. 50 символів).");
            }
            _lastName = value;
        }
    }

    public string LicenseNumber
    {
        get => _licenseNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Номер ліцензії не може бути порожнім.");
            }
            _licenseNumber = value;
        }
    }

    public string Phone
    {
        get => _phone;
        set
        {
            if (value == null || value.Length != 10)
            {
                throw new ArgumentException("Телефон має містити рівно 10 цифр.");
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] < '0' || value[i] > '9')
                {
                    throw new ArgumentException("Телефон має містити тільки цифри.");
                }
            }
            _phone = value;
        }
    }

    public Speciality Speciality { get; set; }
    public WorkSchedule Schedule { get; set; }

    public string FullName => FirstName + " " + LastName;
    public int WorkingHoursPerDay => Schedule.HoursPerDay;
    public string WorkSchedule => Schedule.Display;
    public bool IsAvailableNow => Schedule.IsNow;

    public Doctor() : this("Невідомий", "Лікар", Speciality.General) { }

    public Doctor(string firstName, string lastName, Speciality speciality) : this(firstName, lastName, speciality, "LIC-000", "0000000000") { }

    public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        Schedule = new WorkSchedule(8, 17);
    }

    public bool CanAcceptAt(int hour) => Schedule.Contains(hour);

    public override string ToString()
    {
        string status = IsAvailableNow ? "доступний зараз" : "не в робочий час";
        return "[" + Id + "] " + FullName + " | " + ClinicFormatter.FormatSpeciality(Speciality) + " | " + LicenseNumber + " | Тел:" + ClinicFormatter.FormatPhone(Phone) + " | " + WorkSchedule + " (" + WorkingHoursPerDay + " год) | " + status;
    }
}
