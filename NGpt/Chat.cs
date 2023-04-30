using NGpt.ChatCompletion;
using NGpt.Services.Chat;

namespace NGpt
{
    public class Chat
    {
        ChatService _service;

        public Chat(string apiKey, string organization)
        {
            _service = new ChatService(apiKey, organization);
        }

        public ChatResponse Complete(ChatRequest request)
        {
            var response = _service.Complete(request);

            return response;
        }
    }
}