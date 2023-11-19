using AutoMapper;
using BlogAppExample.BLL.Abstract;
using BlogAppExample.BLL.ResponseConcrete;
using BlogAppExample.DTO.Dtos;
using BlogAppExample.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace BlogAppExample.BLL.Concrete;

public class AccountManager : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public AccountManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _emailService = emailService;
    }

    

    public async Task<Response> Register(AppUserRegisterDto appUserRegisterDto)
    {
        var tempUser = _mapper.Map<AppUser>(appUserRegisterDto);
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
    public async Task<bool>EmailActivation(string token,string userId) 
    {
      var user = await _userManager.FindByIdAsync(userId);
        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded) {return true;}

        if (user.EmailConfirmed)
        {
            return false;
        }

        //tekrardan email gödermek gerekiyor.

        var newToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var link = EmailConfirmLinkGenerator(newToken, user.Id);
        var html = GenerateAccountActivationEmail(link);

        _emailService.SendEmail(user.Email, "Account Activation", html);

        return false;
    
    
    }

    public async Task<Response> Login(AppUserLoginDto appUserLoginDto)
    {
        var appUser = await _userManager.FindByNameAsync(appUserLoginDto.UserName);
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
    public async Task<Response>ForgetPassword(string email) 
    {
      var user = await _userManager.FindByEmailAsync(email);
       if (user == null) 
        {
            return Response.Failure("This Email is not registered");
        
        }

       var ResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var link = ResetPasswordLinkGenerator(ResetToken, user.Id);
        var mailBody = GenerateResetPasswordEmail(link);

        _emailService.SendEmail(user.Email, "Refresh Password", mailBody);
        return Response.Success("Referesh is complated ");
    
    }

    public async Task<Response> VerfyPasswordResetİnfo(string token, string id) 
    {
      var user = await _userManager.FindByIdAsync(id);
        if (user == null) { return Response.Failure("User is not found"); }

        IdentityOptions options = new IdentityOptions();
        var result = await _userManager.VerifyUserTokenAsync(user, options.Tokens.PasswordResetTokenProvider, UserManager<AppUser>.ResetPasswordTokenPurpose, token);

            if (result) 
            {
               return Response.Success("Operation successed");
            }
            else { return Response.Failure("Operation failled"); }
    }

    public async Task<Response> ResetPassword(string newPassword,string id,string token) 
    {
      var user = await _userManager.FindByIdAsync(id);
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        if (result.Succeeded) 
        {
            return Response.Success("Reset password is complated");
        
        }
        return Response.Failure("Error");
        
       
    
    }

    public async Task<Response> Logout()
    {
        await _signInManager.SignOutAsync();
        return Response.Success();
    }
    private string EmailConfirmLinkGenerator(string token,string UserId) 
    {
        var uriBuilder = new UriBuilder("");
        uriBuilder.Path = $"/User/ConfirmEmail";
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["UserId"] = HttpUtility.UrlEncode(UserId);
        queryString["token"] = HttpUtility.UrlEncode(token);
        uriBuilder.Query = queryString.ToString();
        var data = uriBuilder.ToString();

        return data;
    
    }
    private string GenerateAccountActivationEmail(string url) 
    {
        var html = $@"<html><head></head>
                    
                        <body>

                                    <h2>HOŞGELDİN</h2>
                            <a href = {url}> Plase onclik the link to activate Account </a>
                        </body>
    
    
    
                       </html>";

        return html;


    }
    private string GenerateResetPasswordEmail(string url)
    {
        var html = $@"<html><head></head>
                    
                        <body>

                                    <h2>HOŞGELDİN</h2>
                            <a href = {url}> Referesh Password </a>
                        </body>
    
    
    
                       </html>";

        return html;


    }
    private string ResetPasswordLinkGenerator(string token,string UserId)
    {
        var uriBuilder = new UriBuilder("");
        uriBuilder.Path = $"User/ResetPassword";
        var querystring = HttpUtility.ParseQueryString(string.Empty);
        querystring["userID"]= HttpUtility.UrlEncode(UserId);
        querystring["token"]= HttpUtility.UrlEncode(token);
        uriBuilder.Query = uriBuilder.ToString();
        var data = uriBuilder.ToString();

        return data;
    
    
    }
}