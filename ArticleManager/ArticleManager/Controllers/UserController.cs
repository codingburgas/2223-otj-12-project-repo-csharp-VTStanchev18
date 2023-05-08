using ArticleManager.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ArticleManager.Controllers
{
    public class UserController : Controller
    {
        public readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        public User CurrentUser { get; set; }

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
            return View(CurrentUser);
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            CurrentUser = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            TempData["UserName"] = CurrentUser.FirstName + " " + CurrentUser.LastName;
            TempData["UserRole"] = CurrentUser.RoleId;
            ViewBag.Message = CurrentUser;
            //TempData["User"] = user;
            if (CurrentUser != null)
            {
                return RedirectToAction("Index", "Home", CurrentUser);
                // Test(user);
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View(CurrentUser);
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

        [HttpGet]
        public IActionResult EditUser()
        {
            var user = (User)ViewBag.Message;
            return View("UpdateUser", CurrentUser); ;
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            if ((int)TempData["UserRole"] != 1)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("UpdateUser", user);
            }
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
            return View(CurrentUser);
        }
    }
}
