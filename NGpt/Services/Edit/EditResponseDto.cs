using Newtonsoft.Json;

namespace NGpt.Services.Edit;

public class EditResponseDto
{
    [JsonProperty("object")]
    public string Object { get; set; }

    [JsonProperty("created")]
    public int Created { get; set; }

    [JsonProperty("choices")]
    public List<ChoiceDto> Choices { get; set; }

    [JsonProperty("usage")]
    public UsageDto Usage { get; set; }
}

public class ChoiceDto
{
    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("index")]
    public int Index { get; set; }
}

public class UsageDto
{
    [JsonProperty("prompt_tokens")]
    public int PromptTokens { get; set; }

    [JsonProperty("completion_tokens")]
    public int CompletionTokens { get; set; }

    [JsonProperty("total_tokens")]
    public int TotalTokens { get; set; }
}