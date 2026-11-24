using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp;

public class Clinic
{
    public PatientManager Patients { get; set; }
    public DoctorManager Doctors { get; set; }
    public AppointmentManager Appointments { get; set; }
    public MedicalRecordManager MedicalRecords { get; set; }
    public BillingManager Billing { get; set; }
    public AnalyticsManager Analytics { get; set; }
    public ReportManager Reports { get; set; }
    public AppointmentPipeline Pipeline { get; set; }
    public TreatmentPlanManager TreatmentPlans { get; set; }
    public WaitingQueue<Patient> WaitingQueue { get; set; }
    public ClinicLogger Logger { get; set; }
    public ClinicExporter Exporter { get; set; }
    public SessionManager Session { get; set; }
    public PatientPassportWriter Passport { get; set; }
    public SessionEventTracker Tracker { get; set; }

    public Clinic()
    {
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager();
        MedicalRecords = new MedicalRecordManager();
        Billing = new BillingManager(Appointments);
        Analytics = new AnalyticsManager(Appointments, Doctors, Patients);
        Reports = new ReportManager(Appointments, Doctors, Patients);
        Pipeline = new AppointmentPipeline();
        TreatmentPlans = new TreatmentPlanManager();
        WaitingQueue = new WaitingQueue<Patient>();
        Logger = new ClinicLogger();
        Exporter = new ClinicExporter(this);
        Session = new SessionManager();
        Passport = new PatientPassportWriter(this);
        Tracker = new SessionEventTracker(this);
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        Patients.PatientAdded += Logger.OnPatientAdded;
        Appointments.AppointmentBooked += Logger.OnAppointmentBooked;
        Appointments.AppointmentCancelled += Logger.OnAppointmentCancelled;
        Appointments.AppointmentCompleted += Logger.OnAppointmentCompleted;
        Appointments.UrgentAppointmentBooked += Logger.OnUrgentAppointmentBooked;
        Billing.PaymentReceived += Logger.OnPaymentReceived;
        TreatmentPlans.PlanCompleted += Logger.OnPlanCompleted;

        Patients.PatientAdded += Passport.OnPatientAdded;
        Appointments.AppointmentCompleted += Passport.OnAppointmentCompleted;
        TreatmentPlans.PlanCompleted += Passport.OnPlanCompleted;

        Patients.PatientAdded += Tracker.OnPatientAdded;
        Appointments.AppointmentBooked += Tracker.OnAppointmentBooked;
        Appointments.UrgentAppointmentBooked += Tracker.OnUrgentAppointmentBooked;
        Appointments.AppointmentCancelled += Tracker.OnAppointmentCancelled;
        Appointments.AppointmentCompleted += Tracker.OnAppointmentCompleted;
        Billing.PaymentReceived += Tracker.OnPaymentReceived;
        TreatmentPlans.PlanCompleted += Tracker.OnPlanCompleted;
    }
}
