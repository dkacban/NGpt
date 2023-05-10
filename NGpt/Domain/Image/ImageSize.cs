using System.ComponentModel;

namespace NGpt.Domain.Image
{
    public enum ImageSize
    {
        [Description("256x256")]
        Size256x256,

        [Description("512x512")]
        Size512x512,

        [Description("1024x1024")]
        Size1024x1024
    }
}
