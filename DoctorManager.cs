using System;

namespace ClinicApp
{
    public class DoctorManager
    {
        private const int MaxDoctors = 50;
        private Doctor[] _doctors = new Doctor[MaxDoctors];
        private int _count = 0;

        public int Count => _count;

        public void Add(Doctor doctor)
        {
            if (_count >= MaxDoctors)
            {
                Console.WriteLine("Досягнуто ліміт лікарів!");
                return;
            }
            _doctors[_count] = doctor;
            _count++;
            Console.WriteLine($"Лікаря успішно додано. Всього лікарів: {_count}");
        }

        public Doctor? FindById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_doctors[i].Id == id)
                {
                    return _doctors[i];
                }
            }
            return null;
        }

        public Doctor[] FindBySpeciality(string query)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_doctors[i].Speciality.ToLower().Contains(query.ToLower()))
                {
                    matchCount++;
                }
            }

            Doctor[] result = new Doctor[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_doctors[i].Speciality.ToLower().Contains(query.ToLower()))
                {
                    result[index] = _doctors[i];
                    index++;
                }
            }
            return result;
        }

        public Doctor[] GetAll()
        {
            Doctor[] result = new Doctor[_count];
            for (int i = 0; i < _count; i++)
            {
                result[i] = _doctors[i];
            }
            return result;
        }

        public bool Remove(int id)
        {
            int indexToRemove = -1;
            for (int i = 0; i < _count; i++)
            {
                if (_doctors[i].Id == id)
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove == -1) return false;

            for (int i = indexToRemove; i < _count - 1; i++)
            {
                _doctors[i] = _doctors[i + 1];
            }

            _count--;
            _doctors[_count] = null!;
            return true;
        }

        public void DisplayAll()
        {
            Console.WriteLine($"=== Лікарі ({_count} / {MaxDoctors}) ===");
            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_doctors[i]);
            }
        }

        public void DisplayStats()
        {
            int availableNowCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_doctors[i].IsAvailableNow)
                {
                    availableNowCount++;
                }
            }

            Console.WriteLine($"Всього: {_count}, Доступні зараз: {availableNowCount}");

            // Виведення унікальних спеціальностей через три вкладені цикли (без LINQ)
            for (int i = 0; i < _count; i++)
            {
                string currentSpec = _doctors[i].Speciality;

                // Перевіряємо, чи ми вже виводили цю спеціальність раніше
                bool alreadyChecked = false;
                for (int k = 0; k < i; k++)
                {
                    if (_doctors[k].Speciality == currentSpec)
                    {
                        alreadyChecked = true;
                        break;
                    }
                }

                if (alreadyChecked) continue;

                // Рахуємо кількість лікарів для поточної спеціальності
                int specCount = 0;
                for (int j = 0; j < _count; j++)
                {
                    if (_doctors[j].Speciality == currentSpec)
                    {
                        specCount++;
                    }
                }

                Console.WriteLine($"{currentSpec}: {specCount}");
            }
        }
    }
}