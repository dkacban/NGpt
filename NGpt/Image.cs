using NGpt.Domain.Image;
using NGpt.Services.Image;

namespace NGpt
{
    public class Image
    {
        ImageService _service;

        public Image(string apiKey, string organization = "")
        {
            _service = new ImageService(apiKey, organization);
        }

        public ImageResponse Generate(ImageRequest request)
        {
            var response = _service.Generate(request);

            return response;
        }
    }
}
