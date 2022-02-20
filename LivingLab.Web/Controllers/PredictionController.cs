using Microsoft.AspNetCore.Mvc;

namespace LivingLab.Web.Controllers;

public class PredictionController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
