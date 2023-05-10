using System.ComponentModel;
using NGpt.ChatCompletion;

namespace NGpt.Services.Chat
{
    internal class ChatService : BaseService
    {
        public override string Url { get; } = "https://api.openai.com/v1/chat/completions";

        public ChatService(string apiKey, string organization) 
            : base(apiKey, organization)
        {
        }

        public ChatResponse Complete(ChatRequest request)
        {
            var requestDto = new ChatRequestDto
            {
                Messages = request.Messages.Select(m => new ChatMessageDto
                {
                    Role = EnumToString(m.Role),
                    Content = m.Content,
                    Name = null
                }).ToArray(),
                Model = EnumToString(request.Model),
                Temperature = request.Temperature,
                MaxTokens = request.MaxTokens,
                LogitBias = request.LogitBias,
                N = request.N,
                PresencePenalty = request.PresencePenalty,
                Stop = request.Stop,
                Stream = request.Stream,

                TopP = request.TopP,
                User = request.User,
                FrequencyPenalty = request.FrequencyPenalty,
            };

            var responseDto = CallApi<ChatResponseDto>(requestDto);

            var chatResponse = new ChatResponse
            {
                Id = responseDto.Id,
                Choices = responseDto.Choices.Select(c => new Choice()
                {
                    Message = new Message()
                    {
                        Role = GetRoleFromString(c.Message.Role),
                        Content = c.Message.Content
                    },
                    FinishReason = c.FinishReason,
                    Index = c.Index
                }).ToList(),
                Created = responseDto.Created,
                Model = responseDto.Model,
                Object = responseDto.Object,
                Usage = new Usage
                {
                    PromptTokens = responseDto.Usage.PromptTokens,
                    CompletionTokens = responseDto.Usage.CompletionTokens,
                    TotalTokens = responseDto.Usage.TotalTokens
                }
            };

            return chatResponse;
        }

        private Role GetRoleFromString(string roleName)
        {
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                var field = role.GetType().GetField(role.ToString());
                var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

                if ((descriptionAttribute?.Description ?? role.ToString().ToLowerInvariant()).Equals(roleName, StringComparison.OrdinalIgnoreCase))
                {
                    return role;
                }
            }

            throw new ArgumentException($"No matching role found for '{roleName}'.", nameof(roleName));
        }
    }
}
