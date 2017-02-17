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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RMAS.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public SearchController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Search(SearchViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {                
        //        model.SearchResults = _eventRepository.GetEvents(model.EventName, model.EventDate);
        //    }
        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                //If not making web service call uncomment this line:
                //model.SearchResults = _eventRepository.GetEvents(model.EventName, model.EventDate);

                var dateFormated = model.EventDate.HasValue ? model.EventDate.Value.Date.ToString("yyyy-MM-dd") : null;
                var nameFormated = String.IsNullOrEmpty(model.EventName) ? null : model.EventName + "/";
                string requestUri = "api/event/" + nameFormated + dateFormated;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    model.SearchResults = JsonConvert.DeserializeObject<List<Event>>(await response.Content.ReadAsStringAsync());
                }
            }
            return View(model);
        }
    }
}
