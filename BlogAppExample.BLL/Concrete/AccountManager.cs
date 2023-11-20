using AutoMapper;
using BlogAppExample.BLL.Abstract;
using BlogAppExample.BLL.ResponseConcrete;
using BlogAppExample.DTO.Dtos;
using BlogAppExample.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BlogAppExample.BLL.Concrete;

public class AccountManager : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly IUrlHelperFactory _urlHelperFactory;
    private readonly IActionContextAccessor _actionContextAccessor;

    public AccountManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IEmailService emailService, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _emailService = emailService;
        _urlHelperFactory = urlHelperFactory;
        _actionContextAccessor = actionContextAccessor;
    }

    public async Task<Response> Register(AppUserRegisterDto appUserRegisterDto)
    {
        var tempUser = _mapper.Map<AppUser>(appUserRegisterDto);
        tempUser.Name = appUserRegisterDto.Email.Split('@')[0];
        tempUser.SurName = appUserRegisterDto.Email.Split('@')[0];
        tempUser.UserName = appUserRegisterDto.Email.Split('@')[0];
        var result = await _userManager.CreateAsync(tempUser, appUserRegisterDto.Password);
        if (result.Succeeded)
        {
            var registeredUser = await _userManager.FindByEmailAsync(appUserRegisterDto.Email);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(registeredUser);
            var userId = registeredUser.Id;

            var url = EmailConfirmLinkGenerator(token, userId);
            var html = GenerateAccountActivationEmail(url);

            _emailService.SendEmail(appUserRegisterDto.Email, "Email Confirm", html);



            return Response.Success("User registered successfully");
        }
        return Response.Failure(result.Errors);
    }
    public async Task<bool> EmailActivation(string token, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded) { return true; }

        if (user.EmailConfirmed)
        {
            return false;
        }


        var newToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var link = EmailConfirmLinkGenerator(newToken, user.Id);
        var html = GenerateAccountActivationEmail(link);
        var url = GenerateAccountActivationEmail(html);
        _emailService.SendEmail(user.Email, "Account Activation", url);

        return false;


    }
    public async Task<Response> ChangePassword(PasswordUpdateDto pudto)
    {

        var user = await _userManager.FindByIdAsync(pudto.id);

        var control = await _userManager.CheckPasswordAsync(user, pudto.oldPassword);
        if (control)
        {
            var result = await _userManager.ChangePasswordAsync(user, pudto.oldPassword, pudto.newPassword);
            if (!result.Succeeded)
            {
                return Response.Failure("!!!ERROR!!!Password is not changed");

            }
            return Response.Success("Password is changed");
        }
        return Response.Failure("!!!ERROR!!!Password is not changed");
    }


    public async Task<Response> Login(AppUserLoginDto appUserLoginDto)
    {
        var appUser = await _userManager.FindByEmailAsync(appUserLoginDto.Email);
        if (appUser != null)
        {
            var result = await _signInManager.PasswordSignInAsync(appUser, appUserLoginDto.Password, appUserLoginDto.KeepMeLoggedIn,
                false);
            if (result.Succeeded)
                return Response.Success("Login successful");

            if (result.IsLockedOut)
                return Response.Failure("Your account has been locked");
            if (result.IsNotAllowed)
                return Response.Failure("Please confirm your email");

        }
        return Response.Failure("Login Failed");
    }
    public async Task<Response> ForgetPassword(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Response.Failure("This Email is not registered");

        }

        var ResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var link = ResetPasswordLinkGenerator(ResetToken, user.Id);
        var url = GenerateAccountActivationEmail(link);
        _emailService.SendEmail(user.Email, "Reset Password", url);
        return Response.Success("Refresh completed");

    }

    public async Task<Response> VerfyPasswordResetInfo(string token, string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) { return Response.Failure("User is not found"); }

        IdentityOptions options = new IdentityOptions();
        var result = await _userManager.VerifyUserTokenAsync(user, options.Tokens.PasswordResetTokenProvider, UserManager<AppUser>.ResetPasswordTokenPurpose, token);

        if (result)
        {
            return Response.Success("Operation success");
        }
        else { return Response.Failure("Operation failed"); }
    }

    public async Task<Response> ResetPassword(string newPassword, string id, string token)
    {
        var user = await _userManager.FindByIdAsync(id);
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        if (result.Succeeded)
        {
            return Response.Success("Reset password is completed");

        }
        return Response.Failure("Error");
    }

    public async Task<Response> Logout()
    {
        await _signInManager.SignOutAsync();
        return Response.Success();
    }
    private string EmailConfirmLinkGenerator(string token, string UserId)
    {
        var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
        return urlHelper.Action("ConfirmEmail", "User", new { token, UserId }, "https");

    }
    private string GenerateAccountActivationEmail(string url)
    {
        var html = $@"<html><head></head>
                    
                        <body>
                           <h2>Hello To BlogAPP</h2>
                            <a href = {url}> Please click the link to activate Account </a>
                        </body>
                       </html>";

        return html;


    }
    private string GenerateResetPasswordEmail(string url)
    {
        var html = $@"<html><head></head>
                    
                        <body>
                           <h2>Hello To BlogAPP</h2>
                            <a href = {url}> Refresh Password </a>
                        </body>
                       </html>";
        return html;


    }
    private string ResetPasswordLinkGenerator(string token, string UserId)
    {
        var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
        return urlHelper.Action("ResetPassword", "User", new { token, UserId }, "https");
    }
    public async Task<List<string>> AuthorDetail(string id) 
    {
        var author = await _userManager.FindByIdAsync(id);

        if (author != null)
        {
           List<string> authorDetail = new List<string> 
           {
            $"author Name : {author.Name}",
            $"author Surname : {author.SurName}",
            $"author ProfilePicture : {author.ProfilePicture}",
            $"author Description : {author.Description}"

           };
            return authorDetail;
            
        
        }
        return new List<string> {"yazar bulunamadı" };

        
    
    
    }

	
}