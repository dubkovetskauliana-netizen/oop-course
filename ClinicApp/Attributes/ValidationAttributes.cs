using System;

namespace ClinicApp.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class RequiredAttribute : Attribute
{
    public string ErrorMessage { get; }

    public RequiredAttribute(string errorMessage = "Field is required.")
    {
        ErrorMessage = errorMessage;
    }
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class MaxLengthAttribute : Attribute
{
    public int Length { get; }
    public string ErrorMessage { get; }

    public MaxLengthAttribute(int length, string errorMessage = "")
    {
        Length = length;
        ErrorMessage = errorMessage;
    }
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class MinValueAttribute : Attribute
{
    public double Min { get; }
    public string ErrorMessage { get; }

    public MinValueAttribute(double min, string errorMessage = "")
    {
        Min = min;
        ErrorMessage = errorMessage;
    }
}
