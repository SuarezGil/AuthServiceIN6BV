namespace AuthService.Domain.Interfaces;
using AuthService.Domain.Entities;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string name);
    Task <int> CountUsersInRoleAsync(string roleName);
    Task <IReadOnlyList<User>> GetUserByRoleAsync(string roleName);
    Task <IReadOnlyList<string>> GetUserRoleNamesAsync(string userId);
   
}