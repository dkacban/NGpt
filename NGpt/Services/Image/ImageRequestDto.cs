using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NGpt.Services.Image
{
    public class ImageRequestDto
    {
        public ImageRequestDto(string prompt)
        {
            Prompt = prompt;
        }

        [Required]
        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("n")]
        public int N { get; set; } = 1;

        [JsonProperty("size")]
        public string Size { get; set; } = "1024x1024";

        [JsonProperty("response_format")]
        public string ResponseFormat { get; set; } = "url";
        
        [JsonProperty("user")]
        public string User { get; set; }
    }
}
