namespace CaWorkshop.Application.Infrastructure.Identity;

public interface ICurrentUserService
{
    string? UserId { get; }
}
