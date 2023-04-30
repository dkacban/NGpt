using Newtonsoft.Json;

namespace NGpt.Services.Chat;

public class ChoiceDto
{
    [JsonProperty("message")]
    public MessageDto Message { get; set; }

    [JsonProperty("finish_reason")]
    public string FinishReason { get; set; }

    [JsonProperty("index")]
    public int Index { get; set; }
}