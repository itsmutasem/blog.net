using Blog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Blog.Models;

namespace Blog.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Login() => View();

        public IActionResult Signup() => View();

        // POST: Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(string name, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View();
            }

            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                ModelState.AddModelError("", "Email is already registered.");
                return View();
            }

            var user = new User()
            {
                Name = name,
                Email = email,
                Password = HashPassword(password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == hashedPassword);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View();
            }

            // Simple session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", user.Name);

            return RedirectToAction("Index", "Blog");
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
