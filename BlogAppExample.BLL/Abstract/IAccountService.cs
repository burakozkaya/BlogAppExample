using BlogAppExample.BLL.ResponseConcrete;
using BlogAppExample.DTO.Dtos;
using BlogAppExample.Entity.Concrete;

namespace BlogAppExample.BLL.Abstract;

public interface IAccountService
{
    Task<Response> Register(AppUserRegisterDto appUserRegisterDto);
    Task<bool> EmailActivation(string token, string userId);
    Task<Response> Login(AppUserLoginDto appUserLoginDto);
    Task<Response> ForgetPassword(string email);
    Task<Response> VerfyPasswordResetInfo(string token, string id);
    Task<Response> ResetPassword(string newPassword, string id, string token);
    Task<Response> Logout();
     Task<List<string>> AuthorDetail(string id);
    Task<Response> UpdateUser(AppUser appuser, string name, string surname);
}