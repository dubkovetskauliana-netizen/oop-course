using ClinicApp;

WorkSchedule morning = new WorkSchedule(8, 16);
WorkSchedule evening = new WorkSchedule(14, 22);

Console.WriteLine(morning.ToString());
Console.WriteLine("morning.IsNow -> " + morning.IsNow);

Doctor doc = new Doctor("Олег", "Сидоренко", Speciality.Cardiology);
doc.Schedule = evening;
Console.WriteLine(doc.ToString());
