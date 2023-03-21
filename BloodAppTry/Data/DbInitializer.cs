using BloodAppTry.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace BloodAppTry.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BloodContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Admins.Any())
            {
                return;   // DB has been seeded
            }

            var admins = new Admin[]
            {
            new Admin{Username="admin",Password="admin"},
            new Admin{Username="admin2",Password="admin2"}
            };
            foreach (Admin a in admins)
            {
                context.Admins.Add(a);
            }
            context.SaveChanges();

            var bloodBanks = new BloodBank[]
            {
            new BloodBank{Area = "Cluj"},
            new BloodBank{Area = "Sibiu"}
            };
            foreach (BloodBank b in bloodBanks)
            {
                context.BloodBanks.Add(b);
            }
            context.SaveChanges();

            var donationCenters = new DonationCenter[]
            {
            new DonationCenter{Area="Cluj",Address="Zorilor",IsActive=true, OpensAt=8, ClosesAt=16, BloodBankID=1},
            new DonationCenter{Area="Cluj",Address="Manastur",IsActive=true, OpensAt=8, ClosesAt=16, BloodBankID=1}
            };
            foreach (DonationCenter d in donationCenters)
            {
                context.DonationCenters.Add(d);
            }
            context.SaveChanges();

            var doctors = new Doctor[]
            {
            new Doctor{Username="doc",Password="pass", CNP="1750423394877", Email="doc@gmail.com", FirstName="Doc", LastName="Doctorescu", DonationCenterID=1},
            new Doctor{Username="doc2",Password="1234", CNP="1840927394423", Email="doc2@yahoo.com", FirstName="Docky", LastName="Drake", DonationCenterID=2}
            };
            foreach (Doctor d in doctors)
            {
                context.Doctors.Add(d);
            }
            context.SaveChanges();
        }
    }
}
