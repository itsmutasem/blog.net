using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class AuthController : Controller
{
    // GET
    public IActionResult Login()
    {
        return View("Login");
    }
}