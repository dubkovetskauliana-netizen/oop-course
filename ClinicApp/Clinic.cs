using ClinicApp.Managers;

namespace ClinicApp
{
    public class Clinic
    {
        public PatientManager Patients { get; set; }
        public DoctorManager Doctors { get; set; }
        public AppointmentManager Appointments { get; set; }
        public MedicalRecordManager MedicalRecords { get; set; }

        public Clinic()
        {
            Patients = new PatientManager();
            Doctors = new DoctorManager();
            Appointments = new AppointmentManager();
            MedicalRecords = new MedicalRecordManager();
        }
    }
}
