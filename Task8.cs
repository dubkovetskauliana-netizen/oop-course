using System;

namespace oop_course
{
    public static class Task8
    {
        public static double CalculateBMI(double weight, double height)
        {
            return weight / (height * height);
        }

        public static string GetBMICategory(double bmi)
        {
            if (bmi < 18.5) return "недостаня вага";
            if (bmi < 25.0) return "норма";
            if (bmi < 30.0) return "надмірна вага";
            return "ожиріння";
        }

        public static double CalculateCost(double price, int count, int discount)
        {
            double koef = 1.0 - (discount / 100.0);
            return price * count * koef;
        }
        public static string GetAgeCategory(int age)
        {
            if (age <= 17) return "дитина";
            if (age <= 59) return "дорослий";
            return "пенсіонер";
        }

        public static string GetPressureStatus(int systolic, int diastolic)
        {
            if (systolic < 120 && diastolic < 80) return "норма";
            if (systolic < 130 && diastolic < 80) return "підвищений";
            if (systolic < 140 || diastolic < 90) return "гіпертонія 1 ступеня";
            return "гіпертонія 2 ступеня";
        }

        public static void Run()
        {
            Console.Write("Введіть вагу (кг): ");
            double weight = double.Parse(Console.ReadLine()!);

            Console.Write("Введіть зріст (м): ");
            double height = double.Parse(Console.ReadLine()!);

            Console.Write("Введіть ціну прийому (грн): ");
            double price = double.Parse(Console.ReadLine()!);
            Console.Write("Введіть кількість прийомів: ");
            int count = int.Parse(Console.ReadLine()!);

            Console.Write("Введіть знижку (%): ");
            int discount = int.Parse(Console.ReadLine()!);

            Console.Write("Введіть рік народження: ");
            int birthYear = int.Parse(Console.ReadLine()!);

            Console.Write("Введіть систолічний тиск: ");
            int systolic = int.Parse(Console.ReadLine()!);

            Console.Write("Введіть діастолічний тиск: ");
            int diastolic = int.Parse(Console.ReadLine()!);

            double bmi = CalculateBMI(weight, height);
            string bmiCat = GetBMICategory(bmi);
            double totalCost = CalculateCost(price, count, discount);
            int age = 2026 - birthYear;
            string ageCat = GetAgeCategory(age);
            string pressureStatus = GetPressureStatus(systolic, diastolic);

            Console.WriteLine();
            Console.WriteLine($"IMT: {bmi:F2} -> {bmiCat}");
            Console.WriteLine($"Сума: {totalCost:F2} грн");
            Console.WriteLine($"Вік: {age} р., категорія: {ageCat}");
            Console.WriteLine($"Тиск: {systolic}/{diastolic} – {pressureStatus}");
        }
    }
}