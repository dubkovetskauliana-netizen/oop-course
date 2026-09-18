using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class AppointmentManager
{
    private const int MaxAppointments = 500;
    private Appointment[] _appointments = new Appointment[MaxAppointments];
    private int _count = 0;
    private PatientManager _patients;
    private DoctorManager _doctors;

    public int Count => _count;

    public AppointmentManager(PatientManager patients, DoctorManager doctors)
    {
        _patients = patients;
        _doctors = doctors;
    }

    private Appointment? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].Id == id) return _appointments[i];
        }
        return null;
    }

    public bool Book(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes)
    {
        if (_count >= MaxAppointments) return false;
        Patient? patient = _patients.FindById(patientId);
        if (patient == null) return false;
        Doctor? doctor = _doctors.FindById(doctorId);
        if (doctor == null) return false;

        Appointment app = new Appointment(patientId, doctorId, scheduledAt, durationMinutes);
        _appointments[_count] = app;
        _count++;
        return true;
    }

    public bool Cancel(int id, string reason)
    {
        Appointment? app = FindById(id);
        if (app != null) return app.Cancel(reason);
        return false;
    }

    public bool Complete(int id)
    {
        Appointment? app = FindById(id);
        if (app != null) return app.Complete();
        return false;
    }

    public Appointment[] GetByDate(DateTime date)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date) matches++;
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date) { result[index] = _appointments[i]; index++; }
        }
        return result;
    }

    public Appointment[] GetUpcoming()
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming) matches++;
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming) { result[index] = _appointments[i]; index++; }
        }
        return result;
    }

    public void DisplayList(Appointment[] list)
    {
        if (list.Length == 0) return;
        for (int i = 0; i < list.Length; i++)
        {
            Patient? p = _patients.FindById(list[i].PatientId);
            Doctor? d = _doctors.FindById(list[i].DoctorId);
            string pName = p != null ? p.FullName : "Пацієнт";
            string dName = d != null ? d.FullName : "Лікар";
            Console.WriteLine("[" + list[i].Id + "] " + pName + " -> " + dName + " | " + list[i].ScheduledAt.ToString("dd.MM.yyyy HH:mm") + " | " + list[i].Status);
        }
    }
}
