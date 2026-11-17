using ClinicApp.Managers;
using ClinicApp.Utils;

namespace ClinicApp;

public class Clinic
{
    public PatientManager Patients { get; set; }
    public DoctorManager Doctors { get; set; }
    public AppointmentManager Appointments { get; set; }
    public BillingManager Billing { get; set; }
    public AnalyticsManager Analytics { get; set; }
    public TreatmentPlanManager TreatmentPlans { get; set; }
    public ClinicLogger Logger { get; set; }
    public ClinicExporter Exporter { get; set; }
    public SessionManager Session { get; set; }

    public Clinic()
    {
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager();
        Billing = new BillingManager(Appointments);
        Analytics = new AnalyticsManager(Appointments, Doctors, Patients);
        TreatmentPlans = new TreatmentPlanManager();
        Logger = new ClinicLogger();
        Exporter = new ClinicExporter(this);
        Session = new SessionManager();
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
    }
}
