using ArticleManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace ArticleManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveToFile(IFormCollection form)
        {
            // Get the value of the Description field from the form
            string description = form["Description"];

            if (string.IsNullOrEmpty(description))
            {
                ModelState.AddModelError("", "Please enter a paragraph.");
                return View("Index");
            }

            try
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Articles");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, "Article.txt");


                // Create a file stream to write to the file
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // Create a stream writer to write the text to the file
                    using (StreamWriter writer = new StreamWriter(fileStream))
                    {
                        writer.Write(description);
                    }
                }

                return View("Index", description);
            }
            catch (Exception ex)
            {
                // Log the exception
                // ...

                ModelState.AddModelError("", "An error occurred while saving the file. Please try again later.");
                return View("Index");
            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}