using BlogAppExample.BLL.ResponseConcrete;
using BlogAppExample.DTO.Dtos;

namespace BlogAppExample.BLL.Abstract;

public interface IAccountService
{
    Task<Response> Register(AppUserRegisterDto appUserRegisterDto);
    Task<Response> Login(AppUserLoginDto appUserLoginDto);
    Task<Response> Logout();
}