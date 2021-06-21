using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelMngSystem.Models;
using HotelMngSystem.ViewModel;


namespace HotelMngSystem.Controllers
{
    public class BookingController : Controller { 

    private BungalowDBEntities objBungalowDBEntities;


        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }
    }
}