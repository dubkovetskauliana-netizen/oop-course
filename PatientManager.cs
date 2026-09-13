using System;

namespace ClinicApp
{
    public class PatientManager
    {
        private const int MaxPatients = 100;
        private Patient[] _patients = new Patient[MaxPatients];
        private int _count = 0;

        public int Count => _count;

        public void Add(Patient patient)
        {
            if (_count >= MaxPatients)
            {
                Console.WriteLine("Досягнуто ліміт пацієнтів!");
                return;
            }
            _patients[_count] = patient;
            _count++;
            Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FullName} додано.");
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

        public Patient[] FindByName(string query)
        {
            string lowerQuery = query.ToLower();
            int matchCount = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].FirstName.ToLower().Contains(lowerQuery) ||
                    _patients[i].LastName.ToLower().Contains(lowerQuery))
                {
                    matchCount++;
                }
            }

            Patient[] result = new Patient[matchCount];
            int index = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].FirstName.ToLower().Contains(lowerQuery) ||
                    _patients[i].LastName.ToLower().Contains(lowerQuery))
                {
                    result[index] = _patients[i];
                    index++;
                }
            }

            return result;
        }

        public bool Remove(int id)
        {
            int indexToRemove = -1;
            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].Id == id)
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove == -1)
            {
                return false;
            }

            for (int i = indexToRemove; i < _count - 1; i++)
            {
                _patients[i] = _patients[i + 1];
            }

            _count--;
            _patients[_count] = null!;
            return true;
        }

        public void DisplayAll()
        {
            if (_count == 0)
            {
                Console.WriteLine("Порожній список");
                return;
            }

            Console.WriteLine($"=== Пацієнти ({_count} / {MaxPatients}) ===");
            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_patients[i]);
            }
            Console.WriteLine(new string('—', 50));
        }

        public void DisplayStats()
        {
            if (_count == 0)
            {
                Console.WriteLine("Статистика: порожній список.");
                return;
            }

            int totalAge = 0;
            int adultsCount = 0;
            int youngestIndex = 0;
            int oldestIndex = 0;

            for (int i = 0; i < _count; i++)
            {
                int age = _patients[i].Age;
                totalAge += age;

                if (_patients[i].IsAdult)
                {
                    adultsCount++;
                }

                if (_patients[i].Age < _patients[youngestIndex].Age)
                {
                    youngestIndex = i;
                }

                if (_patients[i].Age > _patients[oldestIndex].Age)
                {
                    oldestIndex = i;
                }
            }

            double avgAge = (double)totalAge / _count;

            Console.WriteLine("=== Статистика пацієнтів ===");
            Console.WriteLine($"Всього:          {_count}");
            Console.WriteLine($"Середній вік:    {avgAge:F1} р.");
            Console.WriteLine($"Наймолодший:     {_patients[youngestIndex].FullName} ({_patients[youngestIndex].Age} р.)");
            Console.WriteLine($"Найстарший:      {_patients[oldestIndex].FullName} ({_patients[oldestIndex].Age} р.)");
            Console.WriteLine($"Дорослих:        {adultsCount} з {_count}");
            Console.WriteLine(new string('=', 30));
        }
    }
}