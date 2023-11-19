namespace BlogAppExample.DTO.Dtos;

public class AppUserLoginDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool KeepMeLoggedIn { get; set; }
}