using _app.Models;
using Microsoft.AspNetCore.Mvc;
using Sessions_app.Models;
using System.Diagnostics;

namespace Sessions_app.Controllers
{
    public class HomeController : Controller
    {
        public const string SessionName = "_Nome";
        public const string SessionKey = "_isAuth";

        User auth = new User
        {
            Email = "oalcantara.lucas@gmail.com",
            Password = "123456"
        };

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.SessionName = HttpContext.Session.GetString(SessionName);
            ViewBag.SessionKey = HttpContext.Session.GetInt32(SessionKey);

            return View();
        }
        public IActionResult Login(User request)
        {
            if (request == null) return RedirectToAction("Index");

            if (request.Email == auth.Email)
            {
                if (request.Password == auth.Password)
                {
                    HttpContext.Session.SetString(SessionName, request.Email);
                    HttpContext.Session.SetInt32(SessionKey, 1);
                }
                else
                    HttpContext.Session.SetInt32(SessionKey, 0);
            }
            else
                HttpContext.Session.SetInt32(SessionKey, 0);

            return RedirectToAction("Index");
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
