using System;
using System.Collections.Generic;
using ClinicApp.Enums;
using ClinicApp.Events;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp.Managers;

public class TreatmentPlanManager
{
    private readonly List<TreatmentPlan> _plans = new List<TreatmentPlan>();

    public event EventHandler<TreatmentPlanEventArgs>? PlanCompleted;

    public bool Add(TreatmentPlan plan)
    {
        if (plan == null) return false;

        ValidationResult validationResult = ModelValidator.Validate(plan);
        if (!validationResult.IsValid)
        {
            validationResult.Print();
            return false;
        }

        _plans.Add(plan);
        return true;
    }

    public bool Complete(int planId)
    {
        TreatmentPlan plan = GetById(planId);
        if (plan == null)
        {
            return false;
        }

        if (plan.Complete())
        {
            PlanCompleted?.Invoke(this, new TreatmentPlanEventArgs(plan.Id, plan.PatientId, plan.Diagnosis));
            return true;
        }

        return false;
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
