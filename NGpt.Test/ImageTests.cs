﻿using NGpt.Domain.Image;

namespace NGpt.Test;

public class ImageTests
{
    Image _image;

    public ImageTests()
    {
        _image = new Image();
    }

    [Fact]
    public async void ShouldGenerateImage()
    {
        var imageRequest = new ImageRequest("Little Labrador dog")
        {
            NumberOfImages = 1,
            ResponseFormat = ResponseFormatType.Url,
            Size = ImageSize.Size512x512,
            User = "Dariusz Kacban"
        };

        var response = await _image.GenerateAsync(imageRequest);

        var url = response.Data.First().Url;

        Assert.True(url.Length > 0);
    }

    [Fact]
    public async void ShouldEditImage()
    {
        var imageRequest = new ImageEditRequest("image.png", "Add light blue background")
        {
            NumberOfImages = 1,
            ResponseFormat = ResponseFormatType.Url,
            Size = ImageSize.Size512x512,
            User = "Dariusz Kacban"
        };

        var response = await _image.EditAsync(imageRequest);

        var url = response.Data.First().Url;

        Assert.True(url.Length > 0);
    }
}
