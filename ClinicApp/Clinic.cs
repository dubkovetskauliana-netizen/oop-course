using System;
using ClinicApp.Managers;
using ClinicApp.Models;

namespace ClinicApp;

public class Clinic
{
    public PatientManager Patients { get; }
    public DoctorManager Doctors { get; }
    public object Appointments { get; }
    public object Records { get; }
    public object Billing { get; }
    public WaitingQueue<Patient> WaitingRoom { get; }

    public Clinic()
    {
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = null!;
        Records = null!;
        Billing = null!;
        WaitingRoom = new WaitingQueue<Patient>();
    }
}