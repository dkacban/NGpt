using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NGpt.Services.Image;

public class ImageEditRequestDto
{
    [Required]
    [JsonProperty("image")]
    [StringLength(4000000)]
    [DataType(DataType.ImageUrl)]
    public string Image { get; set; }

    [JsonProperty("mask")]
    [StringLength(4000000)]
    [DataType(DataType.ImageUrl)]
    public string Mask { get; set; }

    [Required]
    [JsonProperty("prompt")]
    [StringLength(1000)]
    public string Prompt { get; set; }

    [JsonProperty("n")]
    [Range(1, 10)]
    public int N { get; set; } = 1;

    [JsonProperty("size")]
    [DataType(DataType.Text)]
    public string Size { get; set; } = "1024x1024";

    [JsonProperty("response_format")]
    [DataType(DataType.Text)]
    public string ResponseFormat { get; set; } = "url";

    [JsonProperty("user")]
    [DataType(DataType.Text)]
    public string User { get; set; }
}
