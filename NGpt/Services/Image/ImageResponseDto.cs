using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGpt.Services.Image
{
    public class ImageResponseDto
    {
        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("data")]
        public List<ImageDataDto> Data { get; set; }
    }

    public class ImageDataDto
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
