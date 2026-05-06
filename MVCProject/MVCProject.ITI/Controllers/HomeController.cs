using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCProject.ITI.Models;

namespace MVCProject.ITI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // if the user is already logged in
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Dashboard", "Dashboard");

        return View();
    }

    public IActionResult Privacy()
    {
        //  redirect authenticated users too
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Dashboard", "Dashboard");

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
