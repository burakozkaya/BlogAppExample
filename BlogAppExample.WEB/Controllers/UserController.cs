using BlogAppExample.BLL.Abstract;
using BlogAppExample.BLL.ResponseConcrete;
using BlogAppExample.DTO.Dtos;
using BlogAppExample.Entity.Concrete;
using BlogAppExample.WEB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppExample.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IBlogContentService _blogContentService;
        private readonly UserManager<AppUser> _userManager;

        public UserController(IAccountService accountService, IBlogContentService blogContentService, UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _blogContentService = blogContentService;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AppUserRegisterDto appUserRegisterDto)
        {
            Response result = new Response();
            if (ModelState.IsValid)
            {
                result = await _accountService.Register(appUserRegisterDto);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Login", "User");
                }
                ViewBag.IsSuccess = result.IsSuccess;
                ViewBag.Message = result.Message;
            }

            foreach (var identityError in result.Errors)
            {
                ModelState.AddModelError("", identityError.Description);
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppUserLoginDto appUserLoginDto)
        {
            Response result = new Response();
            if (ModelState.IsValid)
            {
                result = await _accountService.Login(appUserLoginDto);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.IsSuccess = result.IsSuccess;
                ViewBag.Message = result.Message;
            }
            foreach (var identityError in result.Errors)
            {
                ModelState.AddModelError("", identityError.Description);
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("Insert", "Blog");
        }
        public async Task<IActionResult> ConfirmEmail(string token, string userID)
        {
            var result = await _accountService.EmailActivation(token, userID);
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> ResetPassword(string token, string UserId)
        {
            var result = await _accountService.VerfyPasswordResetInfo(token, UserId);
            TempData["token"] = token;
            TempData["UserId"] = UserId;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(string newPassword)
        {
            var token = TempData["token"].ToString();
            var UserId = TempData["UserId"].ToString();
            var result = await _accountService.ResetPassword(newPassword, UserId, token);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return PartialView("_ForgetPasswordPartial");
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (email != null)
            {
                var result = await _accountService.ForgetPassword(email);

                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = "Your password has been reset successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = result.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Email can not be empty.";
            }

            return RedirectToAction("Login", "User");
        }
        public async Task<IActionResult> AuthorDetail(string id)
        {
            var result = await _accountService.AuthorDetail(id);
            if (result.IsSuccess)
            {
                var blogsResult = _blogContentService.GetUserBlog(id);
                if (blogsResult.IsSuccess)
                {
                    TempData["AuthorBlogs"] = blogsResult.Data;
                    return View(result.Data);
                }
                else
                {
                    TempData["ErrorMessage"] = blogsResult.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }

            return View();
        }
        public async Task<IActionResult> UpdateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            UserUpdateModel model = new UserUpdateModel();
            model.Id = user.Id;
            model.Name = user.Name;
            model.SurName = user.SurName;
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(AppUser appuser, string name, string surname)
        {
            var result = await _accountService.UpdateUser(appuser, name, surname);
            if (result.IsSuccess)
            {
                return View(result);

            }
            return RedirectToAction("Index");

        }
    }
}
