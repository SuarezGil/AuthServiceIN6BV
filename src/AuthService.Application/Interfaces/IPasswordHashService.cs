namespace AuthService.Application.Interfaces;

public interface IPasswordHashService
{
    string hashPassword(string password);
    bool VerifyPassword(string password, string hashPassword);
}