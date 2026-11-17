using System;
using System.IO;
using System.Text;

namespace ClinicApp.Utils;

public class ClinicLogger
{
    private string logPath;

    public ClinicLogger(string logPath = "clinic.log")
    {
        this.logPath = logPath;
    }

    private void WriteLog(string level, string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string line = $"[{timestamp}] [{level}] {message}{Environment.NewLine}";
        File.AppendAllText(logPath, line, Encoding.UTF8);
    }

    public void LogInfo(string message)
    {
        WriteLog("INFO ", message);
    }

    public void LogWarning(string message)
    {
        WriteLog("WARN ", message);
    }

    public void LogError(string message)
    {
        WriteLog("ERROR", message);
    }

    public string[] GetLastLines(int n)
    {
        if (!Exists() || n <= 0)
        {
            return new string[0];
        }

        string[] allLines = File.ReadAllLines(logPath, Encoding.UTF8);
        int skip = allLines.Length - n;
        if (skip < 0)
        {
            skip = 0;
        }

        int count = allLines.Length - skip;
        string[] result = new string[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = allLines[skip + i];
        }

        return result;
    }

    public void Clear()
    {
        if (Exists())
        {
            File.Delete(logPath);
        }
    }

    public bool Exists()
    {
        return File.Exists(logPath);
    }
}
