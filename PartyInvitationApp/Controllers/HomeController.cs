//DRASTI PATEL
//PROBLEM ANALYSIS 2
//MARCH 09, 2025 

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PartyInvitationApp.Models;

namespace PartyInvitationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Constructor: Initializes the logger for debugging/logging purposes
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Handles requests to the homepage ("/")
        public IActionResult Index()
        {
            // Check if the user has visited the site before by looking for the "FirstVisit" cookie
            if (!Request.Cookies.ContainsKey("FirstVisit"))
            {
                // If the user is visiting for the first time, store the current date & time in a cookie
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30) // Set cookie to expire in 30 Days
                };

                // Save the visit timestamp in the cookie
                Response.Cookies.Append("FirstVisit", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"), cookieOptions);
            }
            // Return the "Index" view (Home Page)
            return View();
        }

        // Handles requests to the "Privacy" page ("/Home/Privacy")
        public IActionResult Privacy()
        {
            return View();
        }

        // Handles error pages and logs any error that occurs in the application
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Creates an ErrorViewModel with the current request ID for error tracking
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}