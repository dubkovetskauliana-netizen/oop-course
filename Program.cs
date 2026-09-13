using System;

namespace ClinicApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Doctor doc1 = new Doctor("Олег", "Сидоренко", "Кардіологія", "LIC-001", "0441234567", 8, 16);
            Doctor doc2 = new Doctor("Наталія", "Мороз", "Неврологія", "LIC-002", "0442345678", 9, 18);
            Doctor doc3 = new Doctor("Андрій", "Власенко", "Педіатрія");

            Console.WriteLine(doc1);
            Console.WriteLine(doc2);
            Console.WriteLine(doc3);
        }
    }
}