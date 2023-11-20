using BlogAppExample.BLL.Abstract;
using BlogAppExample.DTO.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BlogAppExample.WEB.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private IBlogContentService _blogContentService;
        private ICategoryService _categoryService;

        public BlogController(ICategoryService categoryService, IBlogContentService blogContentService)
        {
            _categoryService = categoryService;
            _blogContentService = blogContentService;
        }
        public IActionResult Insert()
        {
            ViewBag.CategoryList = _categoryService.GetAll().Data.Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString()
            });
            return View();
        }

        [HttpPost]
        public IActionResult Insert(BlogContentCreateDto contentCreateDto)
        {
            contentCreateDto.CreatedDate = DateTime.Now;
            contentCreateDto.AppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            contentCreateDto.NumberOfReads = 0;

            var result = _blogContentService.Insert(contentCreateDto);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "Blog post has been successfully inserted.";
            }
            else
            {
                TempData["ErrorMessage"] = "Blog post insertion failed: " + result.Message;
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
