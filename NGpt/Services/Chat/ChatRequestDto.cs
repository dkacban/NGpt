using Flurl;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NGpt.Services.Chat
{
    public class ChatRequestDto
    {
        [Required]
        [JsonProperty("model")]
        public string Model { get; set; }

        [Required]
        [JsonProperty("messages")]
        public ChatMessageDto[] Messages { get; set; }

        [JsonProperty("temperature")]
        public float Temperature { get; set; } = 1f;

        [DefaultValue(1f)]
        [JsonProperty("top_p", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float TopP { get; set; } = 1f;

        [DefaultValue(1)]
        [JsonProperty("n", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int N { get; set; } = 1;

        [DefaultValue(false)]
        [JsonProperty("stream", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool Stream { get; set; } = false;

        [JsonProperty("stop", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string[] Stop { get; set; }

        [JsonProperty("max_tokens", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int MaxTokens { get; set; }
        public bool ShouldSerializeMaxTokens()
        {
            return MaxTokens != 0;
        }

        [Range(-2.0, 2.0)]
        [DefaultValue(0f)]
        [JsonProperty("presence_penalty")]
        public float PresencePenalty { get; set; } = 0f;

        [Range(-2.0, 2.0)]
        [DefaultValue(0f)]
        [JsonProperty("frequency_penalty")]
        public float FrequencyPenalty { get; set; } = 0f;

        [JsonProperty("logit_bias", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Dictionary<string, float> LogitBias { get; set; }

        [JsonProperty("user", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string User { get; set; }
    }

    public class ChatMessageDto
    {
        [Required]
        [JsonProperty("role", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Role { get; set; }

        [Required]
        [JsonProperty("content", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Content { get; set; }

        [JsonProperty("name", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}
