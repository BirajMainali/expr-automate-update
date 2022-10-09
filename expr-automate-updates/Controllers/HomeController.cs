using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using expr_automate_updates.Models;
using expr_automate_updates.Worker;

namespace expr_automate_updates.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly WorkerService _workerService;

    public HomeController(ILogger<HomeController> logger, WorkerService workerService)
    {
        _logger = logger;
        _workerService = workerService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> Test()
    {
        try
        {
            await _workerService.BeginUpdate();
            return Ok(new { success = true });
        }
        catch (Exception e)
        {
            return BadRequest(new
            {
                messsage = e.Message
            });
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}