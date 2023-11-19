using BlogAppExample.BLL.Abstract;
using BlogAppExample.BLL.ResponseConcrete;
using BlogAppExample.DTO.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppExample.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;
        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AppUserRegisterDto appUserRegisterDto)
        {
            BlogAppExample.BLL.ResponseConcrete.Response result = new Response();
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
            BlogAppExample.BLL.ResponseConcrete.Response result = new Response();
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
    }
}
