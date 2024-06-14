namespace InternSystem.Application.Features.Message.Models
{
    public class GetMessageHistoryResponse
    {
        public string IdSender { get; set; }
        public string IdReceiver { get; set; }
        public string MessageText { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
