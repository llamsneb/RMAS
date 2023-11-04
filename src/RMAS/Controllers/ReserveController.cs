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
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> Reserve()
        {
            ReserveViewModel model = new ReserveViewModel();
            model.RoomTypes = await GetRoomTypes();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(ReserveViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Events = await _eventRepository.GetEvents(model.RoomNumber, model.BeginTime, model.EndTime, model.Dates);
               
                if (!model.Events.IsNullOrEmpty())
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes due to conflict with prior scheduled events. Use the Search page to check availability.");
                }
                else
                {
                    foreach (DateOnly date in model.Dates)
                    {
                        Event evt = new Event();
                        evt.UserId = _userManager.GetUserId(User);
                        evt.EventName = model.EventName;
                        evt.RoomNumber = model.RoomNumber;
                        evt.EventDate = date;
                        evt.BeginTime = TimeSpan.Parse(model.BeginTime.ToString());
                        evt.EndTime = TimeSpan.Parse(model.EndTime.ToString());

                        model.Events.Add(evt);
                    }

                    try
                    {
                        await _eventRepository.AddEvents(model.Events);
                        await _eventRepository.Save();
                        model.InfoMessage = "Reservation completed successfully.";
                    }
                    catch (DbUpdateException)
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
                    }
                }                
            }
            model.RoomTypes = await GetRoomTypes();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> GetRoomNumbers(string roomType)
        {
            List<Room> rooms = await _roomRepository.GetRooms();
            List<int> RoomNumbers = rooms.Where(r => r.RoomType == roomType).Select(r => r.RoomNumber).ToList();
            return Json(RoomNumbers);
        }

        public async Task<SelectList> GetRoomTypes()
        {
            var rooms = await _roomRepository.GetRooms();
            return new SelectList(rooms.Select(r => r.RoomType).Distinct().ToList());            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _eventRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
