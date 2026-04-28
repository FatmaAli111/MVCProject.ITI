using Microsoft.AspNetCore.Mvc;

namespace MVCProject.ITI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
