using Microsoft.AspNetCore.Authorization;

namespace NRIAwards.PL.Ui.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}