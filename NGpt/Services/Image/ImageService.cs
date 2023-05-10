using NGpt.Domain.Image;

namespace NGpt.Services.Image
{
    internal class ImageService : BaseService
    {
        public override string Url { get; } = "https://api.openai.com/v1/images/generations";

        public ImageService(string apiKey, string organization) : base(apiKey, organization)
        {
        }

        public ImageResponse Generate(ImageRequest request)
        {
            var requestDto = new ImageRequestDto(request.Prompt)
            {
                N = request.NumberOfImages,
                Size = EnumToString(request.Size),
                ResponseFormat = EnumToString(request.ResponseFormat),
                User = request.User
            };

            var responseDto = CallApi<ImageResponseDto>(requestDto);

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
