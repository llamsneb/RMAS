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
            //ViewData["Message"] = new HtmlString("<h3 class=\"fs-4 fw-normal\">Welcome to my sample application!</h3>"
            //    + "<p>This web app was created using the following technologies:</p>"
            //    + "<ul>"
            //    + "<a href=\"https://dotnet.microsoft.com/en-us/learn/aspnet/what-is-aspnet-core\" class=\"link-light link-offset-2 link-underline-opacity-25 link-underline-opacity-100-hover\">" 
            //    + "<li>C# ASP.NET Core MVC V7.0</li></a>"
            //    + "<a href=\"https://learn.microsoft.com/en-us/ef/\" class=\"link-light link-offset-2 link-underline-opacity-25 link-underline-opacity-100-hover\">"
            //    + "<li>Entity Framework</li></a>"
            //    + "<a href=\"https://learn.microsoft.com/en-us/entra/identity-platform/\" class=\"link-light link-offset-2 link-underline-opacity-25 link-underline-opacity-100-hover\">"
            //    + "<li>Microsoft Identity Platform</li></a>"
            //    + "<a href=\"https://www.microsoft.com/en-us/sql-server/\" class=\"link-light link-offset-2 link-underline-opacity-25 link-underline-opacity-100-hover\">"
            //    + "<li>SQLServer</li></a>"
            //    + "<a href=\"https://jquery.com/\" class=\"link-light link-offset-2 link-underline-opacity-25 link-underline-opacity-100-hover\">"
            //    + "<li>JQuery</li></a>"
            //    + "<a href=\"https://jqueryui.com/\" class=\"link-light link-offset-2 link-underline-opacity-25 link-underline-opacity-100-hover\">"
            //    + "<li>JQuery-UI</li></a>"
            //    + "<a href=\"https://fullcalendar.io/\" class=\"link-light link-offset-2 link-underline-opacity-25 link-underline-opacity-100-hover\">"
            //    + "<li>FullCalendar</li></a>"
            //    + "<a href=\"https://getbootstrap.com/\" class=\"link-light link-offset-2 link-underline-opacity-25 link-underline-opacity-100-hover\">"
            //    + "<li>Bootstrap</li></a>"
            //    );

            //ViewData["Message"] = "Your application description page.";            

            ViewData["Message"] = "I created this application based on an assignment in college. The concept of the which is a simple room reservation system for an organization. It was originally written in PHP, but I have since updated it to C# ASP.NET Core MVC and expanded it's features. I use it as a way to practice implementing new skills, such as technologies or patterns. And also to keep up to date with changes to the latest framework versions.";
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
