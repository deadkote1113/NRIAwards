﻿namespace NRIAwards.PL.UI.Areas.Admin.Controllers;

[Area("Admin")]
public class ErrorController : Controller
{
    public IActionResult Index(int code)
    {
        return View(code);
    }
}
