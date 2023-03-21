using System.ComponentModel.DataAnnotations;

namespace BloodAppTry.Models
{
    public class Admin : User
    {
        public int AdminID { get; set; }
    }
}
