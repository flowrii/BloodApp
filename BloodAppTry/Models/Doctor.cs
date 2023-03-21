
using System.ComponentModel.DataAnnotations;

namespace BloodAppTry.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }    
        public string FirstName { get; set; }       
        public string LastName { get; set; }       
        public string Email { get; set; }
        public string CNP { get; set; }
        public int DonationCenterID { get; set; }

        public DonationCenter DonationCenter { get; set; }
    }
}
