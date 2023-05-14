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
            TempData["Email"] = email;
            TempData.Keep("UserName");
            TempData.Keep("UserRole");
            TempData.Keep("Email");
            ViewBag.Message = CurrentUser;

            if (CurrentUser != null)
            {
                return RedirectToAction("Index", "Home", CurrentUser);
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
        public IActionResult Edit()
        {
            var uMail = TempData["Email"];

            var userEf = _context.Users.Where(u => u.Email == uMail).FirstOrDefault();
            ViewBag.Roles = _context.Roles.ToList();

            return View(userEf);
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Edit", user);
            }
        }

        public IActionResult Index()
        {
            return View(CurrentUser);
        }
    }
}
