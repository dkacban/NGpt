using System;
using System.Linq;
using System.Net.Http;
using System.ComponentModel;
using Newtonsoft.Json;
using Flurl.Http;
using NGpt.ChatCompletion;
using Polly;
using Polly.Extensions.Http;
using System.Text;

namespace NGpt.Services.Chat
{
    public class ChatService
    {
        private string _apiKey;
        private readonly string _organization;
        private const string Url = "https://api.openai.com/v1/chat/completions";

        //private const string CompletionsUrl = "https://api.openai.com/v1/completions";
        //private const string EditsUrl = "https://api.openai.com/v1/edits";
        //private const string TranscriptionsUrl = "https://api.openai.com/v1/audio/transcriptions";
        //private const string TranslationsUrl = "https://api.openai.com/v1/audio/translations";
        //private const string FineTunesUrl = "https://api.openai.com/v1/fine-tunes";
        //private const string EmbeddingsUrl = "https://api.openai.com/v1/embeddings";
        //private const string ModerationsesUrl = "https://api.openai.com/v1/moderations";

        public ChatService(string apiKey, string organization)
        {
            _apiKey = apiKey;
            _organization = organization;
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

            var policy = Policy
                .Handle<AggregateException>(aggregateException =>
                    aggregateException.InnerExceptions.Any(innerException => innerException is FlurlHttpException))
                .Or<FlurlHttpException>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)));

            IFlurlResponse response = null;
            policy.Execute(() =>
            {
                response = Url
                    .WithHeader("Content-Type", "application/json")
                    .WithHeader("Authorization", $"Bearer {_apiKey}")
                    .WithHeader("organization", _organization)
                    .PostJsonAsync(requestDto).Result;
            });

            if(response == null)
            {
                throw new Exception("Failed to send request to OpenAI API.");
            }

            string responseBody = response.ResponseMessage.Content.ReadAsStringAsync().Result;

            ChatResponseDto responseDto = JsonConvert.DeserializeObject<ChatResponseDto>(responseBody);

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

        private string GetModelName(ChatModel model)
        {
            var field = model.GetType().GetField(model.ToString());
            var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(System.ComponentModel.DescriptionAttribute));
            return descriptionAttribute?.Description ?? model.ToString().ToLowerInvariant();
        }
    }
}
