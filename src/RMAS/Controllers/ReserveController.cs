using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RMAS.Models;
using RMAS.Models.ReserveViewModels;
using RMAS.Interfaces;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RMAS.Controllers
{
    [Authorize]
    public class ReserveController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReserveController(IEventRepository eventRepository, IRoomRepository roomRepository, UserManager<ApplicationUser> userManager)
        {
            _eventRepository = eventRepository;
            _roomRepository = roomRepository;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reserve()
        {
            ReserveViewModel model = new ReserveViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reserve(ReserveViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<Event> eventList = new List<Event>();
                foreach (DateTime date in model.Dates)
                {
                    Event evt = new Event();
                    evt.UserId = _userManager.GetUserId(User);
                    evt.EventName = model.EventName;
                    evt.RoomNumber = model.RoomNumber;
                    evt.EventDate = date;
                    evt.BeginTime = TimeSpan.Parse(model.BeginTime.ToString());
                    evt.EndTime = TimeSpan.Parse(model.EndTime.ToString());

                    eventList.Add(evt);
                }

                _eventRepository.AddEvents(eventList);
                return RedirectToAction("Reserve");
            }

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult GetRooms(string roomType)
        {
            var RoomNumbers = _roomRepository.GetRoomNumbers(roomType);
            return Json(RoomNumbers);
        }
    }
}
