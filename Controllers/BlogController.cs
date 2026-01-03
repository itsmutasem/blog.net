using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;
using Blog.Models.ViewModels;
using Blog.Filters;

namespace Blog.Controllers;

public class BlogController : Controller
{
    private readonly AppDbContext _context;

    public BlogController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? category)
    {
        var query = _context.Blogs
            .Include(b => b.Author)
            .AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(b => b.Category == category);
        }

        var blogs = await query.ToListAsync();

        var viewModel = new BlogIndexViewModel
        {
            Blogs = blogs,
            TotalBlogs = await _context.Blogs.CountAsync(),
            TotalUsers = await _context.Users.CountAsync(),
            SelectedCategory = category
        };

        return View(viewModel);
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
    public async Task<IActionResult> Create(BlogPost blogPost)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Auth");

        blogPost.UserId = userId.Value;
        blogPost.CreatedAt = DateTime.Now;

        _context.Blogs.Add(blogPost);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [AdminOnly]
    public async Task<IActionResult> Edit(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null) return NotFound();

        return View(blog);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AdminOnly]
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
    [AdminOnly]
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