using System;
using System.Collections.Generic;

namespace ClinicApp.Utils;

public class ImportResult
{
    private List<string> errors = new List<string>();

    public int Imported { get; private set; }
    public int Skipped { get; private set; }
    public IReadOnlyList<string> Errors => errors;

    public void AddSuccess()
    {
        Imported++;
    }

    public void AddError(int lineNumber, string reason)
    {
        Skipped++;
        errors.Add($"Рядок {lineNumber}: {reason}");
    }

    public void Print()
    {
        Console.WriteLine($"Імпортовано: {Imported} | Пропущено: {Skipped}");
        for (int i = 0; i < errors.Count; i++)
        {
            Console.WriteLine($"  {errors[i]}");
        }
    }
}
