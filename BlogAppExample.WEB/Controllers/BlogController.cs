using BlogAppExample.BLL.Abstract;
using BlogAppExample.DTO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BlogAppExample.WEB.Controllers
{
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
        public IActionResult Insert(BlogContentDTO blogContentDto)
        {
            blogContentDto.CreatedDate = DateTime.Now;
            blogContentDto.AppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _blogContentService.Insert(blogContentDto);
            if (result.IsSuccess)
                ViewBag.IsSuccess = result.IsSuccess;
            return View();
        }
    }
}
