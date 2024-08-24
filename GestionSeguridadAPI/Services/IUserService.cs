using GestionSeguridadAPI.Models;

public interface IUserService
{
    Task<User> AuthenticateAsync(string username, string password);
    Task<bool> RegisterAsync(string adminUsername, string adminPassword, string username, string email, string password);
}
