using BlogAppExample.BLL.ResponseConcrete;
using BlogAppExample.DTO.Dtos;

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
    Task<Response> AuthorDetail(string id);
}