using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RMAS.Models;
using RMAS.Models.SearchViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RMAS.Controllers
{
    public class SearchController : Controller
    {
        private RMAS_dbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SearchController(RMAS_dbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
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
        [ValidateAntiForgeryToken]
        public IActionResult Search(SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                
            }

            return View(model);
        }
    }
}
