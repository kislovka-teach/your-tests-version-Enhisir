using Api.Models;

namespace Api.Services;

public interface IUserRepository
{
    public Task<User?> GetUserByUserNameAsync(string username);
    public Task RegisterUserAsync(User user);
}