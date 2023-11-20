using BlogAppExample.BLL.Abstract;
using BlogAppExample.DTO.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppExample.WEB.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Insert(CategoryDTO categoryDto)
        {
            var temp = _categoryService.Insert(categoryDto);
            return View();
        }
        [AllowAnonymous]
        public IActionResult CategoryList()
        {
            var data = _categoryService.GetAll();
            return View(data.Data);
        }
        public IActionResult Update(int id)
        {
            var data = _categoryService.GetById(id);
            return View(data.Data);
        }
        [HttpPost]
        public IActionResult Update(CategoryDTO c)
        {
            var category = _categoryService.Update(c);
            return RedirectToAction("CategoryList", "Category");
        }
        public IActionResult Delete(int id)
        {
            var cat = _categoryService.GetById(id);
            return RedirectToAction("CategoryList");
        }
    }
}
