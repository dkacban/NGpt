using Newtonsoft.Json;

namespace NGpt.Services.Chat;

public class ChatResponseDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("object")]
    public string Object { get; set; }

    [JsonProperty("created")]
    public long Created { get; set; }

    [JsonProperty("model")]
    public string Model { get; set; }

    [JsonProperty("usage")]
    public UsageDto Usage { get; set; }

    [JsonProperty("choices")]
    public List<ChoiceDto> Choices { get; set; }
}