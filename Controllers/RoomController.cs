using HotelManagement.Models;
using HotelManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagement.Controllers
{
    public class RoomController : Controller
    {
        private HotelDBEntities objHotelDbEntities;

        public RoomController()
        {
            objHotelDbEntities = new HotelDBEntities();
        }

        public ActionResult Index()
        {
            RoomViewModel objRoomViewModel = new RoomViewModel();
            objRoomViewModel.ListOfBookingStatus = (from obj in objHotelDbEntities.BookingStatus
                                                    select new SelectListItem()
                                                    {
                                                        Text = obj.BookingStatus,
                                                        Value = obj.BookingStatusId.ToString()
                                                    }).ToList();

            objRoomViewModel.ListOfRoomType = (from obj in objHotelDbEntities.RoomTypes
                                               select new SelectListItem()
                                               {
                                                   Text = obj.RoomTypeName,
                                                   Value = obj.RoomTypeId.ToString()
                                               }).ToList();
            return View(objRoomViewModel);
        }

        [HttpPost]
        public ActionResult Index(RoomViewModel objRoomViewModel)
        {
            string ImageUniqueName = Guid.NewGuid().ToString();
            string ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
            objRoomViewModel.Image.SaveAs(Server.MapPath("~/RoomImages/" + ActualImageName));

            Room objRoom = new Room()
            {
                RoomNumber = objRoomViewModel.RoomNumber,
                RoomDescription = objRoomViewModel.RoomDescription,
                RoomPrice = objRoomViewModel.RoomPrice,
                BookingStatusId = objRoomViewModel.BookingStatusId,
                IsActive = true,
                RoomImage = ActualImageName,
                RoomCapacity = objRoomViewModel.RoomCapacity,
                RoomTypeId = objRoomViewModel.RoomTypeId
            };

            objHotelDbEntities.Rooms.Add(objRoom);
            objHotelDbEntities.SaveChanges();

            return Json(new {message = "Room successfully added", success = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAllRooms()
        {
            IEnumerable<RoomDetailsViewModel> listOfRoomDetailsViewModels =
                (from objRoom in objHotelDbEntities.Rooms
                 join objBooking in objHotelDbEntities.BookingStatus on objRoom.BookingStatusId equals objBooking.BookingStatusId
                 join objRoomType in objHotelDbEntities.RoomTypes on objRoom.RoomTypeId equals objRoomType.RoomTypeId
                 select new RoomDetailsViewModel()
                 {
                     RoomNumber = objRoom.RoomNumber,
                     RoomDescription = objRoom.RoomDescription,
                     RoomCapacity = objRoom.RoomCapacity,
                     RoomPrice = objRoom.RoomPrice,
                     BookingStatus = objBooking.BookingStatus,
                     RoomType = objRoomType.RoomTypeName,
                     RoomImage = objRoom.RoomImage,
                     RoomId = objRoom.RoomId
                 }).ToList();
            return PartialView("_RoomDetailsPartial",listOfRoomDetailsViewModels);
        }

        [HttpGet]
        public JsonResult EditRoomDetails(int roomId)
        {
            var result  =  objHotelDbEntities.Rooms.Single(i => i.RoomId == roomId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}