using ClinicApp.Managers;

namespace ClinicApp
{
    public class Clinic
    {
        public PatientManager Patients { get; set; }
        public DoctorManager Doctors { get; set; }
        public AppointmentManager Appointments { get; set; }
        public BillingManager Billing { get; }

        public Clinic()
        {
            Patients = new PatientManager();
            Doctors = new DoctorManager();
            Appointments = new AppointmentManager();
            Billing = new BillingManager(Appointments);
        }
    }
}
