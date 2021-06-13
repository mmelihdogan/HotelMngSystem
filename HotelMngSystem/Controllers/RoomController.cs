using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelMngSystem.Models;
using HotelMngSystem.ViewModel;

namespace HotelMngSystem.Controllers
{
    public class RoomController : Controller
    {

        private BungalowDBEntities objBungalowDBEntities;

        public RoomController()
        {
            objBungalowDBEntities = new BungalowDBEntities();
        }

        public ActionResult Index()
        {
            RoomViewModel objRoomViewModel = new RoomViewModel();
            objRoomViewModel.ListOfBookingStatus = (from obj in objBungalowDBEntities.BookingStatus
                                                    select new SelectListItem()
                                                    {
                                                        Text = obj.BookingStatus,
                                                        Value = obj.BookingStatusId.ToString()
                                                    }).ToList();

            objRoomViewModel.ListOfRoomType = (from obj in objBungalowDBEntities.RoomTypes
                                               select new SelectListItem()
                                               {
                                                   Text = obj.RoomTypeName,
                                                   Value = obj.RoomTypeId.ToString()
                                               }).ToList();


            return View(objRoomViewModel); 
        }
    }
}