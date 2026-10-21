using System;
using System.Collections.Generic;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class PatientManager
{
    // Задача 1: Внутрішнє сховище замінено на List<Patient>
    private readonly List<Patient> _patients = new List<Patient>();

    // Задача 1: Повертаємо кількість елементів списку за допомогою вбудованої властивості
    public int Count => _patients.Count;

    public Patient? this[int index]
    {
        get
        {
            if (index >= 0 && index < _patients.Count) return _patients[index];
            return null;
        }
    }

    // Задача 1: Додавання в динамічний список без ручної перевірки ліміту
    public void Add(Patient patient)
    {
        if (patient == null) return;

        _patients.Add(patient);
        Console.WriteLine("Пацієнта [" + patient.Id + "] " + patient.FullName + " додано.");
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

    // Задача 1: Використовуємо проміжний List<Patient> та в кінці повертаємо звичайний масив
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

    // Задача 1: Використовуємо проміжний список і метод .ToArray()
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

    // Задача 1: Видалення зі списку замість ручного зсуву елементів масиву
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

    // Задача 1: Повертаємо масив однією швидкою операцією замість копіювання у циклі
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
