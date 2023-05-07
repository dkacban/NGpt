using System.ComponentModel;
using Newtonsoft.Json;
using Flurl.Http;
using NGpt.ChatCompletion;
using Polly;

namespace NGpt.Services.Chat
{
    internal class ChatService : BaseService
    {
        public override string Url { get; } = "https://api.openai.com/v1/chat/completions";

        //private const string CompletionsUrl = "https://api.openai.com/v1/completions";

        //private const string TranscriptionsUrl = "https://api.openai.com/v1/audio/transcriptions";
        //private const string TranslationsUrl = "https://api.openai.com/v1/audio/translations";
        //private const string FineTunesUrl = "https://api.openai.com/v1/fine-tunes";
        //private const string EmbeddingsUrl = "https://api.openai.com/v1/embeddings";
        //private const string ModerationsesUrl = "https://api.openai.com/v1/moderations";

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
                    Role = GetRoleName(m.Role),
                    Content = m.Content,
                    Name = null
                }).ToArray(),
                Model = GetModelName(request.Model),
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

            string responseBody = CallApi(requestDto);
            var responseDto = JsonConvert.DeserializeObject<ChatResponseDto>(responseBody);

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

        private string GetRoleName(Role role)
        {
            var field = role.GetType().GetField(role.ToString());
            var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(System.ComponentModel.DescriptionAttribute));

            return descriptionAttribute?.Description ?? role.ToString().ToLowerInvariant();
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

        private ChatModel GetModelFromString(string modelName)
        {
            foreach (ChatModel model in Enum.GetValues(typeof(ChatModel)))
            {
                var field = model.GetType().GetField(model.ToString());
                var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

                if ((descriptionAttribute?.Description ?? model.ToString().ToLowerInvariant()).Equals(modelName, StringComparison.OrdinalIgnoreCase))
                {
                    return model;
                }
            }

            throw new ArgumentException($"No matching model found for '{modelName}'.", nameof(modelName));
        }
    }
}
