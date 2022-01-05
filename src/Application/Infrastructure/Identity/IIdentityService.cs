namespace CaWorkshop.Application.Infrastructure.Identity;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<(Result Result, string UserId)> CreateUserAsync(
        string email,
        string userName,
        string password);

    Task<Result> DeleteUserAsync(string userId);
}
