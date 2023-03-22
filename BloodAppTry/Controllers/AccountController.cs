using BloodAppTry.Data;
using BloodAppTry.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BloodAppTry.Controllers
{
    public class AccountController : Controller
    {
        private BloodContext _context;
        
        public AccountController(BloodContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationModel model)
        {
            if (model.Username == "" || model.Email == "" || model.FirstName == "" || model.LastName == "" || model.Password == "" || model.Area == "" || model.BloodGroup == "")
            {
                ModelState.AddModelError("", "Fill all the fields");
            }
            else if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords don't match");
            }
            else
            {
                List<Donor> donors = _context.Donors.ToList();
                bool ok = true;
                for (int i = 0; i < donors.Count(); i++)
                    if (model.Username == donors.ElementAt(i).Username)
                    {
                        ModelState.AddModelError("", "Username already exists!");
                        ok = false;
                    }
                if (ok)
                {
                    var newUser = new Donor { Username = model.Username, Password = model.Password, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Area=model.Area, BloodGroup=model.BloodGroup };
                    _context.Donors.Add(newUser);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public async Task<IActionResult> Login(UserLoginModel model)
        {
            bool ok = false;
            string type = "";
            int id = -1;
            int donationCenterID = -1;

            if (model.Username == "" || model.Password == "")
            {
                ModelState.AddModelError("", "Fill all the fields");
            }
            else
            {
                List<Donor> donors = _context.Donors.ToList();
                for (int i = 0; i < donors.Count(); i++)
                    if (model.Username == donors.ElementAt(i).Username && model.Password == donors.ElementAt(i).Password)
                    {
                        ok = true;
                        type = "Donor";
                        id = donors.ElementAt(i).DonorID;
                    }
                List<Admin> admins = _context.Admins.ToList();
                for (int i = 0; i < admins.Count(); i++)
                    if (model.Username == admins.ElementAt(i).Username && model.Password == admins.ElementAt(i).Password)
                    {
                        ok = true;
                        type = "Admin";
                        id = admins.ElementAt(i).AdminID;
                    }
                List<Doctor> doctors = _context.Doctors.ToList();
                for (int i = 0; i < doctors.Count(); i++)
                    if (model.Username == doctors.ElementAt(i).Username && model.Password == doctors.ElementAt(i).Password)
                    {
                        ok = true;
                        type = "Doctor";
                        id = doctors.ElementAt(i).DoctorID;
                        donationCenterID = doctors.ElementAt(i).DonationCenterID;
                    }
                if (!ok)
                {
                    ModelState.AddModelError("", "Wrong username or password");
                }
                else
                {
                    HttpContext.Session.SetString("Username", model.Username);
                    HttpContext.Session.SetString("Type", type);
                    HttpContext.Session.SetString("ID", id.ToString());

                    if(type=="Doctor")
                    {
                        HttpContext.Session.SetString("DonationCenterID", donationCenterID.ToString());
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("Type");
            HttpContext.Session.Remove("ID");
            return RedirectToAction("Index", "Home");
        }
    }
}

