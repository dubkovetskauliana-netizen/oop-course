using System;

namespace ClinicApp
{
    public class GrowablePatientManager
    {
        private Patient[] _patients;
        private int _count;

        public int Count => _count;
        public int Capacity => _patients.Length;

        public GrowablePatientManager()
        {
            _patients = new Patient[4];
            _count = 0;
        }

        private void Grow()
        {
            int oldCapacity = _patients.Length;
            int newCapacity = oldCapacity * 2;

            Patient[] newPatients = new Patient[newCapacity];
            for (int i = 0; i < _count; i++)
            {
                newPatients[i] = _patients[i];
            }

            _patients = newPatients;
            Console.WriteLine($"Масив заповнений! Розширення: {oldCapacity} -> {newCapacity}");
        }

        public void Add(Patient patient)
        {
            if (_count == _patients.Length)
            {
                Grow();
            }
            _patients[_count] = patient;
            _count++;
        }

        public Patient? FindById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].Id == id)
                {
                    return _patients[i];
                }
            }
            return null;
        }

        public void DisplayAll()
        {
            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_patients[i]);
            }
        }
    }
}