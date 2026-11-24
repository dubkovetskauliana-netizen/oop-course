using System;
using System.Collections.Generic;
using System.Linq;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Managers
{
    public class ReportManager
    {
        private readonly AppointmentManager _appointments;
        private readonly DoctorManager _doctors;
        private readonly PatientManager _patients;

        public ReportManager(AppointmentManager appointments, DoctorManager doctors, PatientManager patients)
        {
            _appointments = appointments;
            _doctors = doctors;
            _patients = patients;
        }

        public IEnumerable<SpecialityReport> GetSpecialityStats()
        {
            var appointmentsList = _appointments.GetAll().Where(a => a != null);

            return _doctors.GetAll()
                .Where(d => d != null)
                .GroupBy(d => d.Speciality)
                .Select(g =>
                {
                    var doctorIds = g.Select(d => d.Id).ToArray();
                    var specialityApps = appointmentsList.Where(a => doctorIds.Contains(a.DoctorId));

                    int doctorCount = g.Count();
                    int appointmentCount = specialityApps.Count();
                    decimal totalRevenue = specialityApps.Sum(a => a.GetCost());

                    return new SpecialityReport(g.Key, doctorCount, appointmentCount, totalRevenue);
                })
                .OrderByDescending(r => r.TotalRevenue);
        }

        public string? FindBusiestDoctorName()
        {
            var analytics = new AnalyticsManager(_appointments, _doctors, _patients);

            return analytics.ComputeDoctorStats()
                .OrderByDescending(s => s.AppointmentCount)
                .FirstOrDefault()
                ?.FullName;
        }

        public IEnumerable<string> GetPatientsWithMultipleVisits(int minVisits)
        {
            var patientGroups = _appointments.GetAll()
                .Where(a => a != null)
                .GroupBy(a => a.PatientId)
                .Where(g => g.Count() >= minVisits);

            var patientsList = _patients.GetAll().Where(p => p != null);

            return patientGroups.Join(
                patientsList,
                g => g.Key,
                p => p.Id,
                (g, p) => p.FullName);
        }

        public IEnumerable<DoctorStats> GetTopEarners(int n)
        {
            var analytics = new AnalyticsManager(_appointments, _doctors, _patients);

            return analytics.ComputeDoctorStats()
                .OrderByDescending(s => s.TotalRevenue)
                .Take(n);
        }

        public bool HasAnyUrgentAppointments()
        {
            return _appointments.GetAll()
                .Any(a => a is UrgentAppointment);
        }

        public IEnumerable<Speciality> GetActiveSpecialities()
        {
            return _doctors.GetAll()
                .Where(d => d != null)
                .Select(d => d.Speciality)
                .Distinct()
                .OrderBy(s => s);
        }

        public IEnumerable<(int Year, int Month, decimal Total)> GetMonthlyRevenue()
        {
            return _appointments.GetAll()
                .Where(a => a != null)
                .GroupBy(a => new { a.ScheduledAt.Year, a.ScheduledAt.Month })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => (g.Key.Year, g.Key.Month, g.Sum(a => a.GetCost())));
        }
    }
}
