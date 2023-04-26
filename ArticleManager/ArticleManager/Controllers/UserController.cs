using ArticleManager.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ArticleManager.Controllers
{
    public class UserController : Controller
    {
        User user1;
        public readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View(user1);
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            TempData["UserName"] = user.FirstName + " " + user.LastName;
            user1 = user;
            if (user != null)
            {
                return RedirectToAction("Index", "Home", user);
                // Test(user);
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View(user);
        }
        public void Test(ArticleManager.Models.User user)
        {
            TempData["MessageUser"] = user;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Message"] = "You have been logged out.";
            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult AdminView()
        {
            // This action can only be accessed by users with the "Admin" role.
            return View();
        }

        [Authorize(Policy = "EditorOrAdmin")]
        public IActionResult EditorView()
        {
            // This action can be accessed by users with either the "Editor" or "Admin" role.
            return View();
        }


        public IActionResult Index()
        {
            return View(user1);
        }
    }
}
