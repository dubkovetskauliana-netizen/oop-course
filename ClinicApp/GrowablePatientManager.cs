namespace ClinicApp;

public class GrowablePatientManager
{
    private Patient[] _patients = new Patient[4];
    private int _count = 0;

    public int Count
    {
        get { return _count; }
    }

    public int Capacity
    {
        get { return _patients.Length; }
    }

    private void Resize()
    {
        int oldCapacity = _patients.Length;
        int newCapacity = oldCapacity * 2;
        Patient[] temp = new Patient[newCapacity];

        for (int i = 0; i < _count; i++)
        {
            temp[i] = _patients[i];
        }

        _patients = temp;
        Console.WriteLine("Масив заповнений! Розширення: " + oldCapacity + " -> " + newCapacity);
    }

    public void Add(Patient patient)
    {
        if (_count == _patients.Length)
        {
            Resize();
        }
        _patients[_count] = patient;
        _count++;
        Console.WriteLine("Додано [" + patient.Id + "]. Розмір: " + _count + " / " + _patients.Length);
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
        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_patients[i].ToString());
        }
    }
}
