﻿using Microsoft.AspNetCore.Mvc;

namespace NRIAwards.PL.UI.Areas.Public.Controllers;

[Area("Public")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
