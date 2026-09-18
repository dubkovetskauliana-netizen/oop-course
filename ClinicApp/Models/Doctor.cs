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
        set { ClinicValidator.ValidateName(value, "Ім'я"); _firstName = value; }
    }

    public string LastName
    {
        get => _lastName;
        set { ClinicValidator.ValidateName(value, "Прізвище"); _lastName = value; }
    }

    public string LicenseNumber
    {
        get => _licenseNumber;
        set { ClinicValidator.ValidateName(value, "Номер ліцензії"); _licenseNumber = value; }
    }

    public string Phone
    {
        get => _phone;
        set { ClinicValidator.ValidatePhone(value); _phone = value; }
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
