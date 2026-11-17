using System;
using ClinicApp.Models;
using ClinicApp.Interfaces;
using ClinicApp.Events;

namespace ClinicApp.Managers
{
    public class BillingManager
    {
        private readonly AppointmentManager _appointmentManager;

        public event EventHandler<PaymentEventArgs>? PaymentReceived;

        public BillingManager(AppointmentManager appointmentManager)
        {
            _appointmentManager = appointmentManager ?? throw new ArgumentNullException(nameof(appointmentManager));
        }

        private IPayable[] FilterUnpaid(Appointment[] baseRecords)
        {
            if (baseRecords == null) return new IPayable[0];

            int count = 0;
            for (int i = 0; i < baseRecords.Length; i++)
            {
                if (baseRecords[i] != null && !baseRecords[i].IsPaid && !baseRecords[i].IsCancelled)
                {
                    count++;
                }
            }

            IPayable[] result = new IPayable[count];
            int index = 0;
            for (int i = 0; i < baseRecords.Length; i++)
            {
                if (baseRecords[i] != null && !baseRecords[i].IsPaid && !baseRecords[i].IsCancelled)
                {
                    result[index] = baseRecords[i];
                    index++;
                }
            }

            return result;
        }

        public IPayable[] GetAllUnpaid()
        {
            return FilterUnpaid(_appointmentManager.GetAll());
        }

        public IPayable[] GetUnpaidByPatient(int patientId)
        {
            Appointment[] all = _appointmentManager.GetAll();
            int count = 0;
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i] != null && all[i].PatientId == patientId) count++;
            }

            Appointment[] patientRecords = new Appointment[count];
            int index = 0;
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i] != null && all[i].PatientId == patientId)
                {
                    patientRecords[index] = all[i];
                    index++;
                }
            }

            return FilterUnpaid(patientRecords);
        }

        public decimal GetTotalDebt()
        {
            IPayable[] unpaid = GetAllUnpaid();
            decimal total = 0m;
            for (int i = 0; i < unpaid.Length; i++)
            {
                if (unpaid[i] != null) total += unpaid[i].GetCost();
            }
            return total;
        }

        public decimal GetPatientDebt(int patientId)
        {
            IPayable[] unpaid = GetUnpaidByPatient(patientId);
            decimal total = 0m;
            for (int i = 0; i < unpaid.Length; i++)
            {
                if (unpaid[i] != null) total += unpaid[i].GetCost();
            }
            return total;
        }

        public bool PayAppointment(int appointmentId)
        {
            Appointment app = _appointmentManager.FindById(appointmentId);
            if (app == null || app.IsPaid || app.IsCancelled)
            {
                return false;
            }
            app.MarkPaid();
            PaymentReceived?.Invoke(this, new PaymentEventArgs(app.Id, app.GetCost()));
            return true;
        }

        public void DisplayUnpaid(IPayable[] items)
        {
            if (items == null || items.Length == 0)
            {
                Console.WriteLine("Немає неоплачених записів.");
                return;
            }

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null) continue;

                string costString = items[i].GetCost().ToString("F2");

                if (items[i] is Appointment appointment)
                {
                    Console.WriteLine($"{appointment} | Сума: {costString} грн");
                }
                else
                {
                    Console.WriteLine($"[{i + 1}] Запис | Сума: {costString} грн");
                }
            }
        }
    }
}
