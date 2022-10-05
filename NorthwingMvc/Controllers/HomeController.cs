using System.Diagnostics;//Activity
using Microsoft.AspNetCore.Mvc;//controller, IActionResult
using NorthwingMvc.Models;//ErrorViewModel

namespace NorthwingMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _logger.LogError("dette er varsling kommer på Norsk !");
        _logger.LogWarning("dette er den første varslingen??");
        _logger.LogWarning("den andre varslingen");
        _logger.LogInformation("befinner jeg meg i index metoden av homecontroller.cs");
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
