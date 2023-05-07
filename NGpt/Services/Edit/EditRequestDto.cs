using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NGpt.Services.Edit
{
    internal class EditRequestDto
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("input")]
        public string Input { get; set; } = string.Empty;

        [JsonProperty("instruction")]
        [Required]
        public string Instruction { get; set; }

        [JsonProperty("n")]
        [Range(1, int.MaxValue, ErrorMessage = "Value for 'n' should be greater than or equal to 1.")]
        public int? N { get; set; }

        [JsonProperty("temperature")]
        [Range(0, 2, ErrorMessage = "Value for 'temperature' should be between 0 and 2.")]
        public double? Temperature { get; set; }

        [JsonProperty("top_p")]
        [Range(0, 1, ErrorMessage = "Value for 'top_p' should be between 0 and 1.")]
        public double? TopP { get; set; }
    }
}
