using ClinicApp.Enums;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp.Managers;

public class DoctorManager
{
    private const int MaxDoctors = 50;
    private Doctor[] _doctors = new Doctor[MaxDoctors];
    private int _count = 0;

    public int Count => _count;

    public Doctor? this[int index]
    {
        get
        {
            if (index >= 0 && index < _count) return _doctors[index];
            return null;
        }
    }

    public void Add(Doctor doctor)
    {
        if (_count < MaxDoctors)
        {
            _doctors[_count] = doctor;
            _count++;
        }
    }

    public Doctor? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id) return _doctors[i];
        }
        return null;
    }

    public bool TryFindById(int id, out Doctor doctor)
    {
        Doctor? found = FindById(id);
        if (found != null)
        {
            doctor = found;
            return true;
        }
        doctor = null!;
        return false;
    }

    public Doctor[] FindBySpeciality(Speciality speciality)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality == speciality) matches++;
        }

        Doctor[] result = new Doctor[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality == speciality) { result[index] = _doctors[i]; index++; }
        }
        return result;
    }

    public Doctor[] FindBySpeciality(string query)
    {
        string search = query.ToLower();
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].FirstName.ToLower().Contains(search) || _doctors[i].LastName.ToLower().Contains(search) || ClinicFormatter.FormatSpeciality(_doctors[i].Speciality).ToLower().Contains(search)) matches++;
        }

        Doctor[] result = new Doctor[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].FirstName.ToLower().Contains(search) || _doctors[i].LastName.ToLower().Contains(search) || ClinicFormatter.FormatSpeciality(_doctors[i].Speciality).ToLower().Contains(search)) { result[index] = _doctors[i]; index++; }
        }
        return result;
    }

    public Doctor[] GetAll()
    {
        Doctor[] result = new Doctor[_count];
        for (int i = 0; i < _count; i++) result[i] = _doctors[i];
        return result;
    }

    public bool Remove(int id)
    {
        int index = -1;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id) { index = i; break; }
        }
        if (index == -1) return false;
        for (int i = index; i < _count - 1; i++) _doctors[i] = _doctors[i + 1];
        _count--;
        _doctors[_count] = null!;
        return true;
    }

    public void DisplayAll()
    {
        if (_count == 0) return;
        for (int i = 0; i < _count; i++) Console.WriteLine(_doctors[i].ToString());
    }
}
