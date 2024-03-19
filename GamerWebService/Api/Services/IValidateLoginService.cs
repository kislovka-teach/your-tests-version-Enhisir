using Api.Models;

namespace Api.Services;

public interface IValidateLoginService
{
    public Task<Player?> ValidateLoginAsync(string username, string password);
}