using System;
using System.Globalization;

// Встановлюємо крапку як роздільник для double, щоб програма не падала через коми
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

// 1. Зчитуємо кількість елементів N
int n = int.Parse(Console.ReadLine());
double[] weights = new double[n];

// 2. Заповнюємо масив через звичайний цикл
for (int i = 0; i < n; i++)
{
    weights[i] = double.Parse(Console.ReadLine());
}

// 3. Перший прохід: шукаємо мінімум, максимум та суму
double sum = 0;
double min = weights[0];
double max = weights[0];

foreach (double w in weights)
{
    sum += w;
    if (w < min) min = w;
    if (w > max) max = w;
}

// 4. Рахуємо середнє значення
double average = sum / n;

// 5. Другий прохід: рахуємо скільки пацієнтів мають вагу вище середньої
int aboveAverageCount = 0;
foreach (double w in weights)
{
    if (w > average)
    {
        aboveAverageCount++;
    }
}

// 6. Вивід результату чітко за форматом завдання (один знак після крапки :F1)
Console.WriteLine($"Кількість: {n} / Середня вага: {average:F1} кг / Мін / Макс: {min:F1} / {max:F1} кг / Вище середнього: {aboveAverageCount} з {n}");
