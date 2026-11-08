using ClinicApp.Enums;

namespace ClinicApp.Models;

public class TreatmentPlan
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Diagnosis { get; set; }
    public int DurationDays { get; set; }
    public TreatmentStatus Status { get; set; } = TreatmentStatus.Created;

    public bool Activate()
    {
        if (Status == TreatmentStatus.Created)
        {
            Status = TreatmentStatus.Active;
            return true;
        }
        return false;
    }

    public bool Complete()
    {
        if (Status == TreatmentStatus.Active)
        {
            Status = TreatmentStatus.Completed;
            return true;
        }
        return false;
    }

    public bool Cancel()
    {
        if (Status == TreatmentStatus.Created || Status == TreatmentStatus.Active)
        {
            Status = TreatmentStatus.Cancelled;
            return true;
        }
        return false;
    }
}
