namespace ClinicApp;

public class AppointmentManager
{
    private const int MaxAppointments = 500;
    private Appointment[] _appointments = new Appointment[MaxAppointments];
    private int _count = 0;
    private PatientManager _patients;
    private DoctorManager _doctors;

    public int Count
    {
        get { return _count; }
    }

    public AppointmentManager(PatientManager patients, DoctorManager doctors)
    {
        _patients = patients;
        _doctors = doctors;
    }

    private Appointment? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].Id == id)
            {
                return _appointments[i];
            }
        }
        return null;
    }

    public bool Book(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes)
    {
        if (_count >= MaxAppointments)
        {
            Console.WriteLine("Досягнуто ліміту записів.");
            return false;
        }

        Patient? patient = _patients.FindById(patientId);
        if (patient == null)
        {
            Console.WriteLine("Помилка: пацієнта з ID " + patientId + " не знайдено.");
            return false;
        }

        Doctor? doctor = _doctors.FindById(doctorId);
        if (doctor == null)
        {
            Console.WriteLine("Помилка: лікаря з ID " + doctorId + " не знайдено.");
            return false;
        }

        Appointment app = new Appointment(patientId, doctorId, scheduledAt, durationMinutes);
        _appointments[_count] = app;
        _count++;

        Console.Write("Запис [" + app.Id + "] створено: ");
        DisplayAppointment(app);
        return true;
    }

    public bool Cancel(int id, string reason)
    {
        Appointment? app = FindById(id);
        if (app != null)
        {
            bool res = app.Cancel(reason);
            if (res)
            {
                Console.WriteLine("Запис [" + id + "] скасовано.");
            }
            return res;
        }
        return false;
    }

    public bool Complete(int id)
    {
        Appointment? app = FindById(id);
        if (app != null)
        {
            bool res = app.Complete();
            if (res)
            {
                Console.WriteLine("Запис [" + id + "] завершено.");
            }
            return res;
        }
        return false;
    }

    public Appointment[] GetByPatient(int patientId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                matches++;
            }
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                result[index] = _appointments[i];
                index++;
            }
        }
        return result;
    }

    public Appointment[] GetByDoctor(int doctorId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                matches++;
            }
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                result[index] = _appointments[i];
                index++;
            }
        }
        return result;
    }

    public Appointment[] GetByDate(DateTime date)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                matches++;
            }
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                result[index] = _appointments[i];
                index++;
            }
        }
        return result;
    }

    public Appointment[] GetUpcoming()
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                matches++;
            }
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                result[index] = _appointments[i];
                index++;
            }
        }
        return result;
    }

    public void DisplayAppointment(Appointment app)
    {
        Patient? p = _patients.FindById(app.PatientId);
        Doctor? d = _doctors.FindById(app.DoctorId);

        string pName = p != null ? p.FullName : "Пацієнт #" + app.PatientId;
        string dName = d != null ? d.FullName : "Лікар #" + app.DoctorId;

        Console.Write("[" + app.Id + "] " + pName + " -> " + dName + " | " +
                      app.ScheduledAt.ToString("dd.MM.yyyy HH:mm") + "-" + app.EndsAt.ToString("HH:mm") + " | " + app.Status);

        if (app.Notes.Length > 0)
        {
            Console.Write(" | " + app.Notes);
        }
        Console.WriteLine();
    }

    public void DisplayList(Appointment[] list)
    {
        if (list.Length == 0)
        {
            Console.WriteLine("не знайдено");
            return;
        }

        for (int i = 0; i < list.Length; i++)
        {
            DisplayAppointment(list[i]);
        }
    }
}
