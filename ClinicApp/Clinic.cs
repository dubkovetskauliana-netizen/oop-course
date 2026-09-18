namespace ClinicApp;

public class Clinic
{
    public string Name { get; }
    public PatientManager Patients { get; }
    public DoctorManager Doctors { get; }
    public AppointmentManager Appointments { get; }

    public Clinic(string name)
    {
        Name = name;
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager(Patients, Doctors);
    }

    public void DisplaySchedule(DateTime date)
    {
        Console.WriteLine("=== Розклад на " + date.ToString("dd.MM.yyyy") + " ===");
        Appointment[] dayAppointments = Appointments.GetByDate(date);
        Appointments.DisplayList(dayAppointments);
    }

    public void GenerateReport()
    {
        Appointment[] upcoming = Appointments.GetUpcoming();
        Doctor[] allDoctors = Doctors.GetAll();

        Console.WriteLine("┌──────────────────────────────────────────┐");
        Console.WriteLine("│ Звіт – " + Name.PadRight(34) + "│");
        Console.WriteLine("├──────────────────────────────────────────┤");
        Console.WriteLine("│  Пацієнтів:        " + Patients.Count.ToString().PadRight(22) + "│");
        Console.WriteLine("│  Лікарів:          " + Doctors.Count.ToString().PadRight(22) + "│");
        Console.WriteLine("│  Майбутніх записів: " + upcoming.Length.ToString().PadRight(21) + "│");
        Console.WriteLine("├──────────────────────────────────────────┤");
        Console.WriteLine("│  Навантаження лікарів (майбутні записи): │");

        for (int i = 0; i < allDoctors.Length; i++)
        {
            int docAppointmentsCount = 0;
            for (int j = 0; j < upcoming.Length; j++)
            {
                if (upcoming[j].DoctorId == allDoctors[i].Id)
                {
                    docAppointmentsCount++;
                }
            }
            string docLine = "    " + allDoctors[i].FullName + " (" + allDoctors[i].Speciality + "): " + docAppointmentsCount + " записів";
            Console.WriteLine("│ " + docLine.PadRight(41) + "│");
        }
        Console.WriteLine("└──────────────────────────────────────────┘");
    }
}
