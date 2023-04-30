using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NGpt.ChatCompletion
{
    public class ChatRequest
    {
        [Required]
        [JsonPropertyName("model")]
        [Description("The ChatGPT model to use for generating responses. Use ChatModel enum to choose the value.")]
        public ChatModel Model { get; set; }

        [Required]
        [JsonPropertyName("messages")]
        [Description("Chat messages which are the conversation between User and Assistant so far. Array is useful because you can send more than 1 message in 1 request.")]
        public ChatMessage[] Messages { get; set; }

        [DefaultValue(1f)]
        [JsonPropertyName("temperature")]
        [Description("The value between 0 and 1. Higher values produce more random, creative results.")]
        public float Temperature { get; set; } = 1f;

        [DefaultValue(1f)]
        [JsonPropertyName("top_p")]
        [Description("Controls the nucleus sampling parameter. Lower values produce more focused results.")]
        public float TopP { get; set; } = 1f;

        [DefaultValue(1)]
        [JsonPropertyName("n")]
        [Description("How many chat completion choices to generate for each input message.")]
        public int N { get; set; } = 1;

        [DefaultValue(false)]
        [JsonPropertyName("stream")]
        [Description("Set to true if the request should be streamed.")]
        public bool Stream { get; set; } = false;

        [JsonPropertyName("stop")]
        [Description("An array of stop tokens that indicate the end of a generated response.")]
        public string[] Stop { get; set; }

        [JsonPropertyName("max_tokens")]
        [Description("The maximum number of tokens to generate. You can use this property to lower API usage costs by limitting your tokens consumption.")]
        public int MaxTokens { get; set; }

        [Range(-2.0, 2.0)]
        [DefaultValue(0f)]
        [JsonPropertyName("presence_penalty")]
        [Description("Controls the penalty applied to new tokens based on their presence in the input.")]
        public float PresencePenalty { get; set; } = 0f;

        [Range(-2.0, 2.0)]
        [DefaultValue(0f)]
        [JsonPropertyName("frequency_penalty")]
        [Description("Controls the penalty applied to new tokens based on their frequency in the training data.")]
        public float FrequencyPenalty { get; set; } = 0f;

        [JsonPropertyName("logit_bias")]
        [Description("A dictionary of token biases to apply during generation.")]
        public Dictionary<string, float> LogitBias { get; set; }

        [JsonPropertyName("user")]
        [Description("An optional user identifier.")]
        public string User { get; set; }
    }

    public class ChatMessage
    {
        [Required]
        [JsonPropertyName("role")]
        [Description("The role of the message sender, either 'User' or 'Assistant'.")]
        public Role Role { get; set; }

        [Required]
        [JsonPropertyName("content")]
        [Description("The content of the message.")]
        public string Content { get; set; }

        [JsonPropertyName("name")]
        [Description("An optional name for the sender.")]
        public string Name { get; set; }
    }
}
