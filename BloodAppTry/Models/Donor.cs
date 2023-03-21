﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BloodAppTry.Models
{
    public class Donor
    {
        public int DonorID { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Area { get; set; }
        public string? BloodGroup { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
