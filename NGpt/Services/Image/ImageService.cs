using Flurl.Http;
using Newtonsoft.Json;
using NGpt.Domain.Image;
using Polly.Retry;

namespace NGpt.Services.Image
{
    internal class ImageService : BaseService
    {
        public override string Url { get; } = "https://api.openai.com/v1/images/generations";

        public ImageService(string apiKey, string organization) : base(apiKey, organization)
        {
        }

        public async Task<ImageResponse> Generate(ImageRequest request)
        {
            var requestDto = new ImageRequestDto(request.Prompt)
            {
                N = request.NumberOfImages,
                Size = EnumToString(request.Size),
                ResponseFormat = EnumToString(request.ResponseFormat),
                User = request.User
            };

            var responseDto = await CallApi<ImageResponseDto>(requestDto);

            var imageResponse = new ImageResponse
            {
                Created = responseDto.Created,
                Data = responseDto.Data.Select(i => new ImageData()
                {
                    Url = i.Url
                }).ToList()
            };
            return imageResponse;
        }
    }
}
