using Microsoft.AspNetCore.Mvc;

namespace NRIAwards.PL.UI.Areas.Public.Controllers;

[Area("Public")]
public class ErrorController : Controller
{
    public IActionResult Index(int code)
    {
        return View(code);
    }
}