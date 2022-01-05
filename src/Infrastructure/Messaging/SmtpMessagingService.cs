using CaWorkshop.Application.Infrastructure.Messaging;

namespace CaWorkshop.Infrastructure.Messaging;

public class SmtpMessagingService : IMessagingService
{
    public async Task SendMessageAsync(MessageDto message)
    {
        await Task.Delay(1000);
        await Task.CompletedTask;
    }
}
