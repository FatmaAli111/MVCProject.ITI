using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.ITI.Controllers
{
    [Authorize]
    public class CompareRouteController : Controller
    {

        public IActionResult CompareRoute()
        {
            return View();
        }
    }
}
