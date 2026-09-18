namespace ClinicApp;

public class PatientManager
{
    private const int MaxPatients = 100;
    private Patient[] _patients = new Patient[MaxPatients];
    private int _count = 0;

    public int Count
    {
        get { return _count; }
    }

    public Patient? this[int index]
    {
        get
        {
            if (index >= 0 && index < _count)
            {
                return _patients[index];
            }
            return null;
        }
    }

    public void Add(Patient patient)
    {
        if (_count < MaxPatients)
        {
            _patients[_count] = patient;
            _count++;
            Console.WriteLine("Пацієнта [" + patient.Id + "] " + patient.FullName + " додано.");
        }
        else
        {
            Console.WriteLine("Досягнуто ліміту пацієнтів.");
        }
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

    public Patient[] FindByName(string name)
    {
        string search = name.ToLower();
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(search) || _patients[i].LastName.ToLower().Contains(search))
            {
                matches++;
            }
        }

        Patient[] result = new Patient[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(search) || _patients[i].LastName.ToLower().Contains(search))
            {
                result[index] = _patients[i];
                index++;
            }
        }
        return result;
    }

    public bool Remove(int id)
    {
        int index = -1;
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
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

        Console.WriteLine("=== Пацієнти (" + _count + " / " + MaxPatients + ") ===");
        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_patients[i].ToString());
        }
    }

    public void DisplayStats()
    {
        if (_count == 0)
        {
            Console.WriteLine("Порожній список");
            return;
        }

        double sumAges = 0;
        int minIndex = 0;
        int maxIndex = 0;
        int adultsCount = 0;

        for (int i = 0; i < _count; i++)
        {
            sumAges += _patients[i].Age;

            if (_patients[i].Age < _patients[minIndex].Age)
            {
                minIndex = i;
            }

            if (_patients[i].Age > _patients[maxIndex].Age)
            {
                maxIndex = i;
            }

            if (_patients[i].IsAdult)
            {
                adultsCount++;
            }
        }

        double averageAge = sumAges / _count;

        Console.WriteLine("=== Статистика пацієнтів ===");
        Console.WriteLine("Всього:        " + _count);
        Console.WriteLine("Середній вік:  " + averageAge.ToString("F1") + " р.");
        Console.WriteLine("Наймолодший:   " + _patients[minIndex].FullName + " (" + _patients[minIndex].Age + " р.)");
        Console.WriteLine("Найстарший:    " + _patients[maxIndex].FullName + " (" + _patients[maxIndex].Age + " р.)");
        Console.WriteLine("Дорослих:      " + adultsCount + " з " + _count);
    }
}
