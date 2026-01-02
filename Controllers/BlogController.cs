using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using BlogEntity = Blog.Models.Blog;

namespace Blog.Controllers;

public class BlogController : Controller
{
    private readonly AppDbContext _context;

    public BlogController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var blogs = await _context.Blogs
            .Include(b => b.Author)
            .ToListAsync();

        return View(blogs);
    }
    
    public async Task<IActionResult> Show(int id)
    {
        var blog = await _context.Blogs
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (blog == null)
            return NotFound();

        return View(blog);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BlogEntity blog)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Auth");

        blog.UserId = userId.Value;
        blog.CreatedAt = DateTime.Now;

        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null) return NotFound();

        return View(blog);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, string title, string description, string category)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null) return NotFound();

        blog.Title = title;
        blog.Description = description;
        blog.Category = category;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var blog = await _context.Blogs
            .Include(b => b.Author) // optional
            .FirstOrDefaultAsync(b => b.Id == id);

        if (blog == null)
            return NotFound();

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}