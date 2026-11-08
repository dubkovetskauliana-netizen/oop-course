using System;
using ClinicApp.Managers;
using ClinicApp.Models;

namespace ClinicApp
{
    public class Clinic
    {
        public PatientManager Patients { get; set; }
        public DoctorManager Doctors { get; set; }
        public AppointmentManager Appointments { get; set; }
        public BillingManager Billing { get; }
        public AnalyticsManager Analytics { get; }

        public Clinic()
        {
            Patients = new PatientManager();
            Doctors = new DoctorManager();
            Appointments = new AppointmentManager();
            Billing = new BillingManager(Appointments);
            Analytics = new AnalyticsManager(Appointments, Doctors, Patients);
        }
    }
}
