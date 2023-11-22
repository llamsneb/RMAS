using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RMAS.Models;
using RMAS.Models.SearchViewModels;
using RMAS.Interfaces;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RMAS.Models.BaseViewModels;
using NUglify.Helpers;
using Microsoft.Extensions.Logging;
using System.Drawing.Printing;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RMAS.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public SearchController(IEventRepository eventRepository, UserManager<ApplicationUser> userManager)
        {
            _eventRepository = eventRepository;
            _userManager = userManager;
        }        

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            SearchViewModel model = new SearchViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResultsPage(string eventName, DateOnly? eventDate, int? pageNumber)
        {
            ViewData["EventName"] = eventName;
            ViewData["EventDate"] = eventDate;
            ViewData["Url"] = Url.Action("ResultsPage", "Search");
            ViewData["UserId"] = _userManager.GetUserId(User);
            int pageSize = 10;
            IQueryable<BaseViewModel.Reservation> events = _eventRepository.GetEvents(eventName, eventDate);
            return PartialView("_EventTable", await PaginatedList<BaseViewModel.Reservation>.CreateAsync(events.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(SearchViewModel model, int? pageNumber)
        {
            if (ModelState.IsValid)
            {               
                if (model.EventName.IsNullOrEmpty() && !model.EventDate.HasValue)
                {
                    model.EventDate = DateOnly.FromDateTime(DateTime.Now);
                }
                
                int pageSize = 10;
                IQueryable<BaseViewModel.Reservation> events = _eventRepository.GetEvents(model.EventName, model.EventDate);
                model.ReservationsPage = await PaginatedList<BaseViewModel.Reservation>.CreateAsync(events.AsNoTracking(), pageNumber ?? 1, pageSize);

                if (model.ReservationsPage == null || !model.ReservationsPage.Any())
                {
                    model.InfoMessage = "No records found.";
                }                
            }

            ViewData["EventName"] = model.EventName;
            ViewData["EventDate"] = model.EventDate;
            ViewData["Url"] = Url.Action("ResultsPage", "Search");
            ViewData["UserId"] = _userManager.GetUserId(User);
            return View(model);
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(SearchViewModel model, int? pageNumber)
        {
            if (ModelState.IsValid)
            {
                var deletes = model.Reservations.Where(i => i.IsChecked == true).Select(e => e.Event).ToList();
                if (deletes.Any())
                {
                    if (deletes.Where(d => d.UserId != _userManager.GetUserId(User)).Any())
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save changes. You can only cancel reservations made under your own account.");
                    }
                    else try
                    {
                        _eventRepository.DeleteEvents(deletes);
                        await _eventRepository.Save();
                        model.InfoMessage = "Selected reservations were canceled successfully.";
                    }
                    catch (DbUpdateException)
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
                    }                    
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No reservations to cancel selected.");
                }                
            }

            ViewData["EventName"] = model.EventName;
            ViewData["EventDate"] = model.EventDate;
            ViewData["Url"] = Url.Action("ResultsPage", "Search");
            ViewData["UserId"] = _userManager.GetUserId(User);

            int pageSize = 10;
            IQueryable<BaseViewModel.Reservation> events = _eventRepository.GetEvents(model.EventName, model.EventDate);
            model.ReservationsPage = await PaginatedList<BaseViewModel.Reservation>.CreateAsync(events.AsNoTracking(), pageNumber ?? 1, pageSize);
            return View("Search", model);            
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Search(SearchViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //If not making web service call uncomment this line:
        //        //model.SearchResults = _eventRepository.GetEvents(model.EventName, model.EventDate);

        //        var dateFormated = model.EventDate.HasValue ? model.EventDate.Value.Date.ToString("yyyy-MM-dd") : null;
        //        var nameFormated = String.IsNullOrEmpty(model.EventName) ? null : model.EventName + "/";
        //        string requestUri = "api/event/" + nameFormated + dateFormated;

        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri("http://localhost:5000/");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage response = await client.GetAsync(requestUri);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            model.SearchResults = JsonConvert.DeserializeObject<List<Event>>(await response.Content.ReadAsStringAsync());
        //        }
        //    }
        //    return View(model);
        //}
                
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
