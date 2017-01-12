using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RMAS.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RMAS.Controllers
{
    public class EventController : Controller
    {
        private RMAS_dbContext _context;

        public EventController(RMAS_dbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reserve()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reserve(Event eventObj)
        {
            if (ModelState.IsValid)
            {
                _context.Event.Add(eventObj);
                _context.SaveChanges();
                return RedirectToAction("Reserve");
            }

            return View(eventObj);
        }
    }
}
