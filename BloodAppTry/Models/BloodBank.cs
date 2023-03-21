namespace BloodAppTry.Models
{
    public class BloodBank
    {
        public int BloodBankID { get; set; }
        public string Area { get; set; }

        public ICollection<DonationCenter> donationCenters { get; set; }
    }
}
