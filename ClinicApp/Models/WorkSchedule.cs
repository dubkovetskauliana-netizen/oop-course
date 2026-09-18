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
        Start = start;
        End = end;
    }

    public bool Contains(int hour) => hour >= Start && hour < End;

    public override string ToString() => Display + " (" + HoursPerDay + " год)";
}
