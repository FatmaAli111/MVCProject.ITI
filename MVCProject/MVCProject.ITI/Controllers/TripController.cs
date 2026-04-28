using Microsoft.AspNetCore.Mvc;

namespace MVCProject.ITI.Controllers
{
    public class TripController : Controller
    {
        public IActionResult History()
        {
            return View();
        }
        public IActionResult CompletionTrip()
        {
            return View();
        }
    }
}
