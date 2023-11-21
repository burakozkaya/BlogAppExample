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

            if (temp.IsSuccess)
            {
                TempData["SuccessMessage"] = temp.Message;
            }
            else
            {
                TempData["ErrorMessage"] = temp.Message;
            }

            return RedirectToAction("CategoryList");
        }

        [Authorize]
        public IActionResult CategoryList()
        {
            var data = _categoryService.GetAll();

            if (!data.IsSuccess)
            {
                TempData["ErrorMessage"] = data.Message;
            }

            return View(data.Data ?? new List<CategoryDTO>());
        }

        [Authorize]
        public IActionResult Update(int id)
        {
            var data = _categoryService.GetById(id);

            if (!data.IsSuccess)
            {
                TempData["ErrorMessage"] = data.Message;
                return RedirectToAction("CategoryList");
            }

            return View(data.Data);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(CategoryDTO c)
        {
            var response = _categoryService.Update(c);

            if (response.IsSuccess)
            {
                TempData["SuccessMessage"] = response.Message;
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
            }

            return RedirectToAction("CategoryList");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var response = _categoryService.Delete(id);

            if (response.IsSuccess)
            {
                TempData["SuccessMessage"] = response.Message;
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
            }

            return RedirectToAction("CategoryList");
        }


    }
}
