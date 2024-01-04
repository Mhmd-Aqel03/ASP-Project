using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballGame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using project_scoo.Data;
using project_scoo.Models;

namespace project_scoo.Controllers
{
    public class PassengersController : Controller
    {
        private readonly SystemDbContext _context;

        public PassengersController(SystemDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Passenger user)
        {
            bool empty = checkEmpty(user);
            bool duplicat = checkUsername(user.Username);

            if (empty)
            {
                if (duplicat)
                {
                    _context.Passenegrs.Add(user);
                    _context.SaveChanges();

                    TempData["Msg"] = "the data was saved";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Msg"] = "Please Change the username";
                    return View();
                }
            }
            else
            {
                TempData["Msg"] = "Please fill all input ";
                return View();
            }



        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login userlogin)
        {
            if (ModelState.IsValid)
            {
                string username = userlogin.Username;
                string password = userlogin.Password;

                Passenger user = _context.Passenegrs.Where(
                     u => u.Username.Equals(username) &&
                     u.Password.Equals(password)
                     ).FirstOrDefault();

                Admin admin = _context.Admins.Where(
                    a => a.Username.Equals(username)
                    &&
                    a.Password.Equals(password)
                    ).FirstOrDefault();




                if (user != null)
                {
                    HttpContext.Session.SetInt32("passId", user.Id);

                    return RedirectToAction("TripList");

                }
                else if (admin != null)
                {

                    HttpContext.Session.SetInt32("adminID", admin.Id);

                    return RedirectToAction("Index", "Buses");
                }
                else
                {
                    TempData["Msg"] = "The user Not Found";
                }


            }
            else
            {

            }
            return View();
        }
        public bool checkUsername(string username)
        { 

            Passenger user = _context.Passenegrs.Where(u => u.Username.Equals(username)).FirstOrDefault();
            if (user != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool checkEmpty(Passenger user)
        {
            if (String.IsNullOrEmpty(user.Username)) return false;
            else if (String.IsNullOrEmpty(user.Password)) return false;
            else if (String.IsNullOrEmpty(user.Email)) return false;
            else if (String.IsNullOrEmpty(user.Name)) return false;
            else if (String.IsNullOrEmpty(user.Email)) return false;
            else return true;
        }
        public IActionResult TripList()
        {
            int id = (int)HttpContext.Session.GetInt32("passId");

            List<int> lst = _context.Passengers_Trips.Where(
                t => t.Passenegr.Id == id
                ).Select(s => s.Trip.Id).ToList();

            List<Trip> lst_Trips = _context.Trips.ToList();

            List<Trip> flist = new List<Trip>();

            for (int i = 0; i < lst_Trips.Count(); i++)
            {
                if (!(lst.Contains(lst_Trips[i].Id)))
                {
                    flist.Add(lst_Trips[i]);
                }
            }

            return View(flist); 
        }
        public IActionResult BookedList()
        {
            int userid = (int)HttpContext.Session.GetInt32("passId");

            List<int> lst = _context.Passengers_Trips.Where(
                t => t.Passenegr.Id == userid
                ).Select(s => s.Trip.Id).ToList();

            List<Trip> lst_trips = _context.Trips.Where(
                t => lst.Contains(t.Id)
                ).ToList();

            return View(lst_trips);
        }
        public IActionResult Book(int id)
        {
            int TripId = id;
            int UserId = (int)HttpContext.Session.GetInt32("passId");

            Passenger_trips pass_trip = new Passenger_trips();

            pass_trip.Passenegr = _context.Passenegrs.Find(UserId);
            pass_trip.Trip = _context.Trips.Find(TripId);
            
            _context.Passengers_Trips.Add(pass_trip);
            _context.SaveChanges();

            return RedirectToAction("BookedList");
        }
        public IActionResult Delete(int id)
        {
            int tid = _context.Passengers_Trips.Where(
                t => t.Trip.Id == id
                ).Select(s => s.Id).FirstOrDefault();

            Passenger_trips trip = _context.Passengers_Trips.Find(tid);
            _context.Passengers_Trips.Remove(trip);

            _context.SaveChanges();

            return RedirectToAction("BookedList");
        }
    }
}
