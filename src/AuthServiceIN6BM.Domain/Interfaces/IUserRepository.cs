using AuthServiceIN6BM.Domain.Entities;

namespace AuthServiceIN6BM.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<User> GetUserAsync(string id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailVerificationTokenAsync(string token);
    Task<User?> GetByPasswordResetTokenAsync(string token);
    Task<bool> ExistByEmailAsync(string email);
    Task<bool> ExistByUsernameAsync(string username);
    Task<User> updateAsync(User user);
    Task<bool> DeleteAsync(string id);
    Task UpdateUserRoleAsync(string userId, string roleId);
}