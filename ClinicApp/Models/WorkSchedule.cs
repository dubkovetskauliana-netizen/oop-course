using ClinicApp.Utils;

namespace ClinicApp.Models;

public struct WorkSchedule
{
    public int Start { get; }
    public int End { get; }

    public int HoursPerDay => End - Start;
    public string Display => Start.ToString("D2") + ":00–" + End.ToString("D2") + ":00";
    public bool IsNow => Contains(DateTime.Now.Hour);

    public WorkSchedule(int start, int end)
    {
        if (start < 0 || start > 23)
        {
            throw new ArgumentOutOfRangeException(nameof(start), "Година початку має бути від 0 до 23.");
        }
        if (end < 1 || end > 24)
        {
            throw new ArgumentOutOfRangeException(nameof(end), "Година завершення має бути від 1 до 24.");
        }
        if (start >= end)
        {
            throw new ArgumentException("Година початку не може бути більшою або рівною годині завершення.");
        }

        Start = start;
        End = end;
    }

    public bool Contains(int hour) => hour >= Start && hour < End;

    public override string ToString() => Display + " (" + HoursPerDay + " год)";
}
