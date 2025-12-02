using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.Web.Controllers;

public class Test : Controller
{
    public IActionResult Index()
    {
        var model = new Models.TestViewModel
        {
            Name = "Jules",
            DateOfBirth = new DateTime(1983, 8, 31)
        };
        return View(model);
    }
}
