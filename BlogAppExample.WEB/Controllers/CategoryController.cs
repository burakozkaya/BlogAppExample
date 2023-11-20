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
        [Authorize]
        public IActionResult Insert()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Insert(CategoryDTO categoryDto)
        {
            var temp = _categoryService.Insert(categoryDto);
            return View();
        }
        [Authorize]
        public IActionResult CategoryList()
        {
            var data=_categoryService.GetAll();
            return View(data);
        }
        [Authorize]
        public IActionResult Update(int id)
        {
            var data = _categoryService.GetById(id);
            return View("CategoryUpdate",data);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Update(CategoryDTO c)
        {
            var category = _categoryService.Update(c);
            return View();
        }
        [Authorize]
        public IActionResult Delete(int id)
        {
            var cat = _categoryService.GetById(id);
            return RedirectToAction("CategoryList");
        }
    }
}
