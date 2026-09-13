using System;

namespace ClinicApp
{
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
            Console.WriteLine($"=== Розклад на {date:dd.MM.yyyy} ===");
            var apps = Appointments.GetByDate(date);
            Appointments.DisplayList(apps);
        }

        public void GenerateReport()
        {
            var upcomingApps = Appointments.GetUpcoming();
            var allDoctors = Doctors.GetAll();

            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║ Звіт – {Name,-47} ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ Пацієнтів:          {Patients.Count,-32} ║");
            Console.WriteLine($"║ Лікарів:            {Doctors.Count,-32} ║");
            Console.WriteLine($"║ Майбутніх записів:  {upcomingApps.Length,-32} ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ Навантаження лікарів (майбутні записи):                  ║");

            foreach (var doc in allDoctors)
            {
                int count = 0;
                foreach (var app in upcomingApps)
                {
                    if (app.DoctorId == doc.Id)
                    {
                        count++;
                    }
                }
                string line = $"  {doc.FullName} ({doc.Speciality}): {count} записів";
                Console.WriteLine($"║ {line,-56} ║");
            }

            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        }
    }
}