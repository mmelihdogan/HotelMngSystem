using System;
using System.IO;
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
        [HttpPost]

        public ActionResult Index(RoomViewModel objRoomViewModel) {

            // objBungalowDBEntities
            Room objRoom = new Room()
            {
                RoomNumber = objRoomViewModel.RoomNumber,
                RoomDescription = objRoomViewModel.RoomDescription,
                RoomPrice = objRoomViewModel.RoomPrice,
                BookingStatusId = objRoomViewModel.BookingStatusId,
                IsActive = true,
                RoomCapacity = objRoomViewModel.RoomCapacity,
                RoomTypeId = objRoomViewModel.RoomTypeId
            };
            objBungalowDBEntities.Rooms.Add(objRoom);
            objBungalowDBEntities.SaveChanges();

            return Json(new {message = "Room Successfully Added.", success = true}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllRooms()
        {
            IEnumerable<RoomDetailsViewModel> listOfRoomDetailsViewModels =
                (from objRoom in objBungalowDBEntities.Rooms
                 join objBooking in objBungalowDBEntities.BookingStatus on objRoom.BookingStatusId equals objBooking.BookingStatusId
                 join objRoomType in objBungalowDBEntities.RoomTypes on objRoom.RoomTypeId equals objRoomType.RoomTypeId
                 select new RoomDetailsViewModel()
                 {
                     RoomNumber = objRoom.RoomNumber,
                     RoomDescription = objRoom.RoomDescription,
                     RoomCapacity =objRoom.RoomCapacity,
                     RoomPrice = objRoom.RoomPrice,
                     BookingStatus = objBooking.BookingStatus,
                     RoomType = objRoomType.RoomTypeName,
                     RoomImage = objRoom.RoomImage,
                     RoomId = objRoom.RoomId
                 }).ToList();
            return PartialView("_RoomDetailsPartial", listOfRoomDetailsViewModels);
        }

    }
}