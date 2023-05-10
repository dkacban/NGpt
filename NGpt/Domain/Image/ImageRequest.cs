using System.ComponentModel;

namespace NGpt.Domain.Image
{
    public class ImageRequest
    {
        public ImageRequest(string prompt)
        {
            Prompt = prompt;
        }
        public string Prompt { get; set; }
        public int NumberOfImages { get; set; } = 1;
        public ImageSize Size { get; set; } = ImageSize.Size1024x1024;
        public ResponseFormatType ResponseFormat { get; set; } = ResponseFormatType.Url;
        public string User { get; set; }
    }

    public enum ResponseFormatType
    {
        [Description("url")]
        Url,

        [Description("b64_json")]
        Base64Json
    }
}
