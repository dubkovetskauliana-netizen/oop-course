using System;
using System.Collections.Generic;
using ClinicApp.Enums;
using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class PatientManager
{
    private readonly List<Patient> _patients = new List<Patient>();

    public event EventHandler<PatientEventArgs>? PatientAdded;

    public int Count => _patients.Count;

    public Patient? this[int index]
    {
        get
        {
            if (index >= 0 && index < _patients.Count) return _patients[index];
            return null;
        }
    }

    public void Add(Patient patient)
    {
        if (patient == null) return;

        _patients.Add(patient);
        Console.WriteLine("Пацієнта [" + patient.Id + "] " + patient.FullName + " додано.");
        PatientAdded?.Invoke(this, new PatientEventArgs(patient.Id, patient.FullName));
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].Id == id) return _patients[i];
        }
        return null;
    }

    public bool TryFindById(int id, out Patient patient)
    {
        Patient? found = FindById(id);
        if (found != null)
        {
            patient = found;
            return true;
        }
        patient = null!;
        return false;
    }

    public Patient[] FindByName(string name)
    {
        string search = name.ToLower();
        List<Patient> resultList = new List<Patient>();

        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(search) || _patients[i].LastName.ToLower().Contains(search))
            {
                resultList.Add(_patients[i]);
            }
        }
        return resultList.ToArray();
    }

    public Patient[] FindByBloodType(BloodType bloodType)
    {
        List<Patient> resultList = new List<Patient>();

        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                resultList.Add(_patients[i]);
            }
        }
        return resultList.ToArray();
    }

    public bool Remove(int id)
    {
        Patient? found = FindById(id);
        if (found != null)
        {
            _patients.Remove(found);
            return true;
        }
        return false;
    }

    public Patient[] GetAll()
    {
        return _patients.ToArray();
    }

    public void DisplayAll()
    {
        if (_patients.Count == 0) return;
        for (int i = 0; i < _patients.Count; i++)
        {
            Console.WriteLine(_patients[i].ToString());
        }
    }
}
