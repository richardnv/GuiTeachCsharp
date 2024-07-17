
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GuiTeachWeb.Models;

namespace GuiTeachWeb.Controllers;
public class GuitarNeckController : Controller
{
    private readonly ILogger<GuitarNeckController> _logger;

    public GuitarNeckController(ILogger<GuitarNeckController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {        
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}