using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagement.Controllers
{
    public class BookingController : Controller
    {
        private HotelDBEntities objHotelDbEntities;
        public BookingController() => objHotelDbEntities = new HotelDBEntities();

        public ActionResult Index()
        {
            return View();
        }
    }
}