using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class AboutController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}