using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RMAS.Models;
using RMAS.Models.ReserveViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RMAS.Controllers
{
    public class ReserveController : Controller
    {
        private RMAS_dbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReserveController(RMAS_dbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
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
                foreach (DateTime date in model.Dates)
                {
                    Event evt = new Event();
                    evt.UserId = _userManager.GetUserId(User);
                    evt.EventName = model.EventName;
                    evt.RoomNumber = model.RoomNumber;
                    evt.EventDate = date;
                    evt.BeginTime = TimeSpan.Parse(model.BeginTime.ToString());
                    evt.EndTime = TimeSpan.Parse(model.EndTime.ToString());

                    _context.Event.Add(evt);
                }
                _context.SaveChanges();
                return RedirectToAction("Reserve");
            }

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult GetRooms(string roomType)
        {
            var RoomNumbers = _context.Room.Where(r => r.RoomType == roomType).Select(r => r.RoomNumber).ToList();
            return Json(RoomNumbers);
        }
    }
}
