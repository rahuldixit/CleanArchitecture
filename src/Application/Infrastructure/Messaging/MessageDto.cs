namespace CaWorkshop.Application.Infrastructure.Messaging
{
    public class MessageDto
    {
        public string? SenderId { get; set; }

        public string? RecipientId { get; set; }

        public string? Subject { get; set; }

        public string? Body { get; set; }
    }
}
