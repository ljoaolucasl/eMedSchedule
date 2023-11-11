namespace eMedSchedule.Domain.DoctorModule
{
    public class Doctor : Entity
    {
        public string Name { get; set; }
        public string CRM { get; set; }

        public Doctor()
        {
        }

        public Doctor(string name, string cRM)
        {
            Name = name;
            CRM = cRM;
        }

        public override bool Equals(object? obj)
        {
            return obj is Doctor doctor &&
                   Id.Equals(doctor.Id) &&
                   Name == doctor.Name &&
                   CRM == doctor.CRM;
        }
    }
}