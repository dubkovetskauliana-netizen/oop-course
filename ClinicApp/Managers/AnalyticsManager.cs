using System;
using System.Collections.Generic;
using System.Linq;
using ClinicApp.Models;

namespace ClinicApp.Managers
{
    public class AnalyticsManager
    {
        private readonly AppointmentManager _appointments;
        private readonly DoctorManager _doctors;
        private readonly PatientManager _patients;

        public AnalyticsManager(AppointmentManager appointments, DoctorManager doctors, PatientManager patients)
        {
            _appointments = appointments;
            _doctors = doctors;
            _patients = patients;
        }

        public IEnumerable<DoctorStats> ComputeDoctorStats()
        {
            var appointmentsList = _appointments.GetAll().Where(a => a != null);

            return _doctors.GetAll()
                .Where(d => d != null)
                .Select(d =>
                {
                    var doctorApps = appointmentsList.Where(a => a.DoctorId == d.Id);

                    int count = doctorApps.Count();
                    decimal revenue = doctorApps.Sum(a => a.GetCost());
                    DateTime lastDate = doctorApps.Any()
                        ? doctorApps.Max(a => a.ScheduledAt)
                        : DateTime.MinValue;

                    return new DoctorStats(d.Id, d.FullName, count, revenue, lastDate);
                });
        }

        public IEnumerable<PatientStats> ComputePatientStats()
        {
            var appointmentsList = _appointments.GetAll().Where(a => a != null);

            return _patients.GetAll()
                .Where(p => p != null)
                .Select(p =>
                {
                    var patientApps = appointmentsList.Where(a => a.PatientId == p.Id);

                    int count = patientApps.Count();
                    decimal spent = patientApps.Sum(a => a.GetCost());
                    DateTime lastDate = patientApps.Any()
                        ? patientApps.Max(a => a.ScheduledAt)
                        : DateTime.MinValue;

                    return new PatientStats(p.Id, p.FullName, count, spent, lastDate);
                });
        }
    }
}
