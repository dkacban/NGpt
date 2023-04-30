using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace NGpt.Services.Completion;

public class CompletionRequest
{
    [Required]
    [JsonPropertyName("model")]
    public string Model { get; set; }

    [JsonPropertyName("prompt")]
    public string[] Prompt { get; set; }

    [JsonPropertyName("suffix")]
    public string Suffix { get; set; }

    [DefaultValue(16)]
    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; set; } = 16;

    [DefaultValue(1)]
    [JsonPropertyName("temperature")]
    public float Temperature { get; set; } = 1f;

    [DefaultValue(1)]
    [JsonPropertyName("top_p")]
    public float TopP { get; set; } = 1f;

    [DefaultValue(1)]
    [JsonPropertyName("n")]
    public int N { get; set; } = 1;

    [DefaultValue(false)]
    [JsonPropertyName("stream")]
    public bool Stream { get; set; } = false;

    [JsonPropertyName("logprobs")]
    public int? Logprobs { get; set; }

    [DefaultValue(false)]
    [JsonPropertyName("echo")]
    public bool Echo { get; set; } = false;

    [JsonPropertyName("stop")]
    public string[] Stop { get; set; }

    [Range(-2.0, 2.0)]
    [DefaultValue(0)]
    [JsonPropertyName("presence_penalty")]
    public float PresencePenalty { get; set; } = 0f;

    [Range(-2.0, 2.0)]
    [DefaultValue(0)]
    [JsonPropertyName("frequency_penalty")]
    public float FrequencyPenalty { get; set; } = 0f;

    [DefaultValue(1)]
    [JsonPropertyName("best_of")]
    public int BestOf { get; set; } = 1;

    [JsonPropertyName("logit_bias")]
    public Dictionary<string, float> LogitBias { get; set; }

    [JsonPropertyName("user")]
    public string User { get; set; }
}
