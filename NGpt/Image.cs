using NGpt.Domain.Image;
using NGpt.Services.Image;

namespace NGpt
{
    public class Image
    {
        ImageService _service;
        ImageEditService _imageEditService;

        public Image(string apiKey, string organization = "")
        {
            _service = new ImageService(apiKey, organization);
            _imageEditService = new ImageEditService(apiKey, organization);
        }

        public async Task<ImageResponse> GenerateAsync(ImageRequest request)
        {
            var response = await _service.Generate(request);

            return response;
        }
        public async Task<ImageResponse> EditAsync(ImageEditRequest request)
        {
            var response = await _imageEditService.Edit(request);

            return response;
        }

    }
}
