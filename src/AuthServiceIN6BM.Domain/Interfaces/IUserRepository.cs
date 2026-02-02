using AuthServiceIN6BM.Domain.Entities;
namespace AuthServiceIN6BM.Domain.Entities;
 
 
public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<User> GetByIdAsyn(string id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<User> GetByEmailVerificationTokenAsync(string token);
    Task<User> GetByPasswordResetTokenAsync(string token);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(string id);
 
    Task UpdateUserRoleAsync(string UserId, string roleId);
 
 
 
}