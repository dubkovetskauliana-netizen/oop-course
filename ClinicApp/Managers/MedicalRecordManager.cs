using System;
using ClinicApp.Models;

namespace ClinicApp.Managers
{
    public class MedicalRecordManager
    {
        private readonly MedicalRecord[] _records = new MedicalRecord[1000];
        private int _count = 0;

        public int Count
        {
            get { return _count; }
        }

        public void Add(MedicalRecord record)
        {
            if (record == null) return;

            if (_count >= 1000)
            {
                Console.WriteLine("Помилка: досягнуто ліміту записів (1000).");
                return;
            }

            _records[_count] = record;
            _count++;
            Console.WriteLine($"Запис [{record.Id}] {record.GetRecordType()} додано.");
        }

        public MedicalRecord FindById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].Id == id)
                {
                    return _records[i];
                }
            }
            return null;
        }

        public MedicalRecord[] GetByPatient(int patientId)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId)
                {
                    matchCount++;
                }
            }

            MedicalRecord[] result = new MedicalRecord[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId)
                {
                    result[index] = _records[i];
                    index++;
                }
            }
            return result;
        }

        public MedicalRecord[] GetByDoctor(int doctorId)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].DoctorId == doctorId)
                {
                    matchCount++;
                }
            }

            MedicalRecord[] result = new MedicalRecord[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].DoctorId == doctorId)
                {
                    result[index] = _records[i];
                    index++;
                }
            }
            return result;
        }

        // --- Нові методи Задачі 3 ---

        public Diagnosis[] GetDiagnoses(int patientId)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is Diagnosis)
                {
                    matchCount++;
                }
            }

            Diagnosis[] result = new Diagnosis[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is Diagnosis diag)
                {
                    result[index] = diag;
                    index++;
                }
            }
            return result;
        }

        public LabResult[] GetLabResults(int patientId)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is LabResult)
                {
                    matchCount++;
                }
            }

            LabResult[] result = new LabResult[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is LabResult lr)
                {
                    result[index] = lr;
                    index++;
                }
            }
            return result;
        }

        public Prescription[] GetPrescriptions(int patientId)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is Prescription)
                {
                    matchCount++;
                }
            }

            Prescription[] result = new Prescription[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is Prescription pr)
                {
                    result[index] = pr;
                    index++;
                }
            }
            return result;
        }

        public Diagnosis[] GetChronicDiagnoses(int patientId)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is Diagnosis diag && diag.IsChronic)
                {
                    matchCount++;
                }
            }

            Diagnosis[] result = new Diagnosis[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is Diagnosis diag && diag.IsChronic)
                {
                    result[index] = diag;
                    index++;
                }
            }
            return result;
        }

        public Prescription[] GetActivePrescriptions(int patientId)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is Prescription pr && pr.IsActive())
                {
                    matchCount++;
                }
            }

            Prescription[] result = new Prescription[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is Prescription pr && pr.IsActive())
                {
                    result[index] = pr;
                    index++;
                }
            }
            return result;
        }

        public void DisplayPatientSummary(int patientId)
        {
            int totalRecords = 0;
            int diagnosesCount = 0;
            int labResultsCount = 0;
            int prescriptionsCount = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId)
                {
                    totalRecords++;
                    if (_records[i] is Diagnosis) diagnosesCount++;
                    else if (_records[i] is LabResult) labResultsCount++;
                    else if (_records[i] is Prescription) prescriptionsCount++;
                }
            }

            if (totalRecords == 0)
            {
                Console.WriteLine("Записів не знайдено.");
                return;
            }

            Console.WriteLine($"=== Медична картка пацієнта #{patientId} ===");
            Console.WriteLine($"Всього записів: {totalRecords} (діагнозів: {diagnosesCount}, аналізів: {labResultsCount}, рецептів: {prescriptionsCount})");

            Diagnosis[] chronic = GetChronicDiagnoses(patientId);
            if (chronic.Length > 0)
            {
                Console.WriteLine($"Хронічні діагнози ({chronic.Length}):");
                for (int i = 0; i < chronic.Length; i++)
                {
                    Console.WriteLine($"  {chronic[i]}");
                }
            }

            Prescription[] active = GetActivePrescriptions(patientId);
            if (active.Length > 0)
            {
                Console.WriteLine($"Активні рецепти ({active.Length}):");
                for (int i = 0; i < active.Length; i++)
                {
                    Console.WriteLine($"  {active[i]} | до {active[i].ExpiresAt.ToString("dd.MM.yyyy")}");
                }
            }
        }

        // --- Старі методи відображення ---

        public void DisplayAll()
        {
            if (_count == 0)
            {
                Console.WriteLine("Медична картка порожня.");
                return;
            }

            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_records[i]);
            }
        }

        public void DisplayList(MedicalRecord[] records)
        {
            if (records == null || records.Length == 0)
            {
                Console.WriteLine("Записів не знайдено.");
                return;
            }

            for (int i = 0; i < records.Length; i++)
            {
                Console.WriteLine(records[i]);
            }
        }

        public MedicalRecord this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    return null;
                }
                return _records[index];
            }
        }
    }
}
