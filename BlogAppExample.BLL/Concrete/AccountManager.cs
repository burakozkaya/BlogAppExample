using AutoMapper;
using BlogAppExample.BLL.Abstract;
using BlogAppExample.BLL.ResponseConcrete;
using BlogAppExample.DTO.Dtos;
using BlogAppExample.Entity.Concrete;
using Microsoft.AspNetCore.Identity;

namespace BlogAppExample.BLL.Concrete;

public class AccountManager : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;

    public AccountManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<Response> Register(AppUserRegisterDto appUserRegisterDto)
    {
        var tempUser = _mapper.Map<AppUser>(appUserRegisterDto);
        var result = await _userManager.CreateAsync(tempUser, appUserRegisterDto.Password);
        if (result.Succeeded)
        {
            return Response.Success("Kullanıcı Ekleme Başarılı");
        }
        return Response.Failure("Kullanıcı Eklemedi");
    }

    public async Task<Response> Login(AppUserLoginDto appUserLoginDto)
    {
        var appUser = await _userManager.FindByNameAsync(appUserLoginDto.UserName);
        if (appUser != null)
        {
            var result = await _signInManager.PasswordSignInAsync(appUser, appUserLoginDto.Password, appUserLoginDto.KeepMeLoggedIn,
                false);
            if (result.Succeeded)
            {
                return Response.Success("Giriş Başarılı");
            }
        }
        return Response.Failure("Giriş Başarısız");
    }

    public async Task<Response> Logout()
    {
        await _signInManager.SignOutAsync();
        return Response.Success();
    }
}