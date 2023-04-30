using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NGpt.Services.Chat;

public class MessageDto
{
    [JsonProperty("role")]
    public string Role { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }
}