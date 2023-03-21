using System.ComponentModel.DataAnnotations;

namespace BloodAppTry.Models
{
    public class User
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
