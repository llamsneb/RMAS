using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace RMAS.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {                    
            ViewData["Message"] = "I created this application based on an assignment in college. " +
                "The concept of which is a simple room reservation system for an organization. " +
                "It was originally written in PHP, but I have since updated it to C# ASP.NET Core MVC and expanded it's features. " +
                "I use it as a way to practice implementing new skills, technologies, " +
                "and stay informed with changes to the latest framework versions.";
            
            return View();
        }

        public IActionResult Contact()
        {
            //ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
