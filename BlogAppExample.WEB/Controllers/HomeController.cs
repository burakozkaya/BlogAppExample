using BlogAppExample.BLL.Abstract;
using BlogAppExample.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogAppExample.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogContentService _blogContentService;

        public HomeController(ILogger<HomeController> logger, IBlogContentService blogContentService)
        {
            _logger = logger;
            _blogContentService = blogContentService;
        }

        public IActionResult Index()
        {
            TempData["MostReaded"] = _blogContentService.GetMostReaded();
            var blogs = _blogContentService.GetAll();
            return View(blogs.Data);
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
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            _blogContentService.GetById(id);
            _blogContentService.IncrementReadCount(id);
            var temp = _blogContentService.GetById(id);
            return View(temp.Data);
        }
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}