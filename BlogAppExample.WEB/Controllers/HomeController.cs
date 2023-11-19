using BlogAppExample.BLL.Abstract;
using BlogAppExample.DTO.Dtos;
using BlogAppExample.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace BlogAppExample.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogContentService _blogContentService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;

        public HomeController(ILogger<HomeController> logger, IBlogContentService blogContentService, ICategoryService categoryService, IAccountService accountService)
        {
            _logger = logger;
            _blogContentService = blogContentService;
            _categoryService = categoryService;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult InsertCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult InsertCategory(CategoryDTO categoryDto)
        {
            var temp = _categoryService.Insert(categoryDto);
            return View();
        }
        public IActionResult InsertBlog()
        {
            ViewBag.CategoryList = _categoryService.GetAll().Data.Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString()
            });
            return View();
        }

        [HttpPost]
        public IActionResult InsertBlog(BlogContentDTO blogContentDto)
        {
            blogContentDto.CreatedDate = DateTime.Now;
            var result = _blogContentService.Insert(blogContentDto);
            if (result.IsSuccess)
                ViewBag.IsSuccess = result.IsSuccess;
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AppUserRegisterDto appUserRegisterDto)
        {
            var result = await _accountService.Register(appUserRegisterDto);
            ViewBag.IsSuccess = result.IsSuccess;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppUserLoginDto appUserLoginDto)
        {
            var result = await _accountService.Login(appUserLoginDto);
            ViewBag.IsSuccess = result.IsSuccess;
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}