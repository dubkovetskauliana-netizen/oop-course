namespace ClinicApp;

public class DoctorManager
{
    private const int MaxDoctors = 50;
    private Doctor[] _doctors = new Doctor[MaxDoctors];
    private int _count = 0;

    public int Count
    {
        get { return _count; }
    }

    public void Add(Doctor doctor)
    {
        if (_count < MaxDoctors)
        {
            _doctors[_count] = doctor;
            _count++;
        }
        else
        {
            Console.WriteLine("Досягнуто ліміту лікарів.");
        }
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

    public Doctor[] FindBySpeciality(string speciality)
    {
        string search = speciality.ToLower();
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality.ToLower() == search)
            {
                matches++;
            }
        }

        Doctor[] result = new Doctor[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality.ToLower() == search)
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
        int index = -1;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            return false;
        }

        for (int i = index; i < _count - 1; i++)
        {
            _doctors[i] = _doctors[i + 1];
        }

        _count--;
        _doctors[_count] = null!;
        return true;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Порожній список");
            return;
        }

        Console.WriteLine("=== Лікарі (" + _count + " / " + MaxDoctors + ") ===");
        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_doctors[i].ToString());
        }
    }

    public void DisplayStats()
    {
        if (_count == 0)
        {
            Console.WriteLine("Порожній список");
            return;
        }

        int availableCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].IsAvailableNow)
            {
                availableCount++;
            }
        }

        Console.WriteLine("=== Статистика лікарів ===");
        Console.WriteLine("Всього:          " + _count);
        Console.WriteLine("Доступні зараз:  " + availableCount);
        Console.WriteLine("По спеціальностях:");

        for (int i = 0; i < _count; i++)
        {
            bool isDuplicate = false;
            for (int j = 0; j < i; j++)
            {
                if (_doctors[i].Speciality.ToLower() == _doctors[j].Speciality.ToLower())
                {
                    isDuplicate = true;
                    break;
                }
            }

            if (!isDuplicate)
            {
                int specCount = 0;
                for (int k = 0; k < _count; k++)
                {
                    if (_doctors[k].Speciality.ToLower() == _doctors[i].Speciality.ToLower())
                    {
                        specCount++;
                    }
                }
                Console.WriteLine("  " + _doctors[i].Speciality + ": " + specCount);
            }
        }
    }
}
