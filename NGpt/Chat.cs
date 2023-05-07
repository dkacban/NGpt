using NGpt.ChatCompletion;
using NGpt.Services.Chat;
using System;

namespace NGpt
{
    public class Chat
    {
        ChatService _service;

        public Chat(string apiKey, string organization = "")
        {
            _service = new ChatService(apiKey, organization);
        }

        public ChatResponse Complete(ChatRequest request)
        {
            var response = _service.Complete(request);

            return response;
        }

        public string Complete(string request)
        {

            var completionRequest = new ChatRequest()
            {
                Messages = new ChatMessage[]
                {
                    new ChatMessage()
                    {
                        Role = Role.User,
                        Content = request,
                    }
                },
                Temperature = 0f,
                Model = ChatModel.GPT3_5Turbo,
                N = 1
            };

            var response = Complete(completionRequest);
            var content = response.Choices[0].Message.Content;

            return content;
        }
    }
}