using NGpt.Domain.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGpt.Test
{
    public class ImageTests
    {
        Image _image;

        public ImageTests()
        {
            _image = new Image("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", "org-UpMJfYAwK3diGzF1OVSVLb1e");
        }

        [Fact]
        public void ShouldCreateUrl()
        {
            var imageRequest = new ImageRequest("Little Labrador dog")
            {
                NumberOfImages = 1,
                ResponseFormat = ResponseFormatType.Url,
                Size = ImageSize.Size512x512,
                User = "Dariusz Kacban"
            };

            var response = _image.Generate(imageRequest);

            var url = response.Data.First().Url;

            Assert.True(url.Length > 0);
        }
    }
}
