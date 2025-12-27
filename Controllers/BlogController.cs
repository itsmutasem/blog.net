using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class BlogsController : Controller
{
    private record BlogListItem(int Id, string Title, string Author);

    public IActionResult Index()
    {
        var blogs = new List<BlogListItem>
        {
            new(1, "Building a Modern ASP.NET Core Blog", "Alice Writer"),
            new(2, "Dark Theme UX Tips for Developers", "Boris Admin"),
            new(3, "Why Dependency Injection Matters", "Carla Viewer")
        };

        return View(blogs);
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Details(int id)
    {
        ViewData["Placeholder"] = id;
        return View();
    }

    public IActionResult Edit(int id)
    {
        ViewData["Placeholder"] = id;
        return View();
    }

    public IActionResult Delete(int id)
    {
        ViewData["Placeholder"] = id;
        return View();
    }
}