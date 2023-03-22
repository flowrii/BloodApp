namespace BloodAppTry.Models
{
    public class DonationCenter
    {
        public int DonationCenterID { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public int OpensAt { get; set; }
        public int ClosesAt { get; set; }
        public int BloodBankID { get; set; }
        public int maxDayAppointments { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public BloodBank BloodBank { get; set; }
    }
}