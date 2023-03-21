namespace BloodAppTry.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DonorID { get; set; }
        public int DonationCenterID { get; set; }
        public DateTime Date { get; set; }

        public Donor Donor { get; set; }
        public DonationCenter DonationCenter { get; set; }
    }
}