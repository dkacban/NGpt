using Flurl.Http;
using Newtonsoft.Json;
using NGpt.Domain.Image;
using Polly.Retry;

namespace NGpt.Services.Image;

internal class ImageEditService : BaseService
{
    public override string Url { get; } = "https://api.openai.com/v1/images/edits";

    public ImageEditService(string apiKey, string organization) : base(apiKey, organization)
    {
    }

    public async Task<ImageResponse> Edit(ImageEditRequest request)
    {
        var requestDto = new ImageEditRequestDto()
        {
            Mask = request.Mask,
            Image = request.Image,
            Prompt = request.Prompt,
            N = request.NumberOfImages,
            Size = EnumToString(request.Size),
            ResponseFormat = EnumToString(request.ResponseFormat),
            User = request.User
        };

        var responseDto = await CallApiPostImages(requestDto);

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

    private async Task<ImageResponseDto> CallApiPostImages(ImageEditRequestDto requestDto)
    {
        AsyncRetryPolicy policy = CreateRetryPolicy();

        IFlurlResponse response = null;
        await policy.ExecuteAsync(async () =>
        {
            response = await Url
                .WithHeader("Authorization", $"Bearer {_apiKey}")
                .WithHeader("organization", _organization)
                .PostMultipartAsync(mp =>
                {
                    mp.AddFile("image", requestDto.Image);
                    mp.AddString("prompt", "add yellow background");
                    if (requestDto.Mask != null) mp.AddFile("mask", requestDto.Mask);
                    if (requestDto.N != 0) mp.AddString("n", requestDto.N.ToString());
                    if (requestDto.Size != null) mp.AddString("size", requestDto.Size);
                    if (requestDto.ResponseFormat != null) mp.AddString("response_format", requestDto.ResponseFormat);
                    if (requestDto.User != null) mp.AddString("user", requestDto.User);
                });
        });

        if (response == null)
        {
            throw new Exception("Failed to send request to OpenAI API.");
        }

        string responseBody = await response.ResponseMessage.Content.ReadAsStringAsync();

        var responseDto = JsonConvert.DeserializeObject<ImageResponseDto>(responseBody);

        return responseDto;
    }
}