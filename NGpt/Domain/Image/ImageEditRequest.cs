namespace NGpt.Domain.Image
{
    public class ImageEditRequest
    {
        public ImageEditRequest(string image, string prompt)
        {
            Prompt = prompt;
            Image = image;
        }
        public string Image { get; set; }
        public string Mask { get; set; }
        public string Prompt { get; set; }
        public int NumberOfImages { get; set; } = 1;
        public ImageSize Size { get; set; } = ImageSize.Size1024x1024;
        public ResponseFormatType ResponseFormat { get; set; } = ResponseFormatType.Url;
        public string User { get; set; }
    }
}
