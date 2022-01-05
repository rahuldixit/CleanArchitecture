namespace CaWorkshop.Application.Infrastructure.Messaging;

public interface IMessagingService
{
    Task SendMessageAsync(MessageDto message);
}
