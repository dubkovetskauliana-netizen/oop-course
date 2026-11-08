using ClinicApp.Enums;
using ClinicApp.Models;
using ClinicApp.Utils;
using System.Collections.Generic;

namespace ClinicApp.Managers;

public class TreatmentPlanManager
{
    private readonly List<TreatmentPlan> _plans = new List<TreatmentPlan>();
    private int _nextId = 1;

    public bool Add(TreatmentPlan plan)
    {
        if (plan == null) return false;

        ValidationResult validationResult = ModelValidator.Validate(plan);
        if (!validationResult.IsValid)
        {
            return false;
        }

        plan.Id = _nextId++;
        _plans.Add(plan);
        return true;
    }

    public TreatmentPlan GetById(int id)
    {
        foreach (var plan in _plans)
        {
            if (plan != null && plan.Id == id)
            {
                return plan;
            }
        }
        return null;
    }

    public TreatmentPlan[] GetByPatient(int patientId)
    {
        int count = 0;
        foreach (var plan in _plans)
        {
            if (plan != null && plan.PatientId == patientId) count++;
        }

        TreatmentPlan[] result = new TreatmentPlan[count];
        int index = 0;
        foreach (var plan in _plans)
        {
            if (plan != null && plan.PatientId == patientId)
            {
                result[index++] = plan;
            }
        }
        return result;
    }

    public TreatmentPlan[] GetByStatus(TreatmentStatus status)
    {
        int count = 0;
        foreach (var plan in _plans)
        {
            if (plan != null && plan.Status == status) count++;
        }

        TreatmentPlan[] result = new TreatmentPlan[count];
        int index = 0;
        foreach (var plan in _plans)
        {
            if (plan != null && plan.Status == status)
            {
                result[index++] = plan;
            }
        }
        return result;
    }

    public TreatmentPlan[] GetAll()
    {
        return _plans.ToArray();
    }
}
