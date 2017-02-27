#r "Microsoft.WindowsAzure.Storage"
#r "System.Drawing"

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using ImageResizer;


// Image resize based on System.Drawing. Do NOT use in production!  
public static void Run(Stream inImage, Stream outThumbnail)
{
    var imageBuilder = ImageResizer.ImageBuilder.Current;
    var size = imageDimensionsTable[ImageSize.ExtraSmall];

    imageBuilder.Build(inImage, outThumbnail, 
        new ResizeSettings(size.Width, size.Height, FitMode.Max, null), false);
}

#region Helpers

public enum ImageSize
{
    ExtraSmall, Small, Medium
}

private static Dictionary<ImageSize, Size> imageDimensionsTable = new Dictionary<ImageSize, Size>()
{
    { ImageSize.ExtraSmall, new Size(320, 200) },
    { ImageSize.Small, new Size(640, 400) },
    { ImageSize.Medium, new Size(800, 600) }
};

private static ImageFormat ScaleImage(Stream blobInput, Stream output, ImageSize imageSize)
{
    ImageFormat imageFormat;

    var size = imageDimensionsTable[imageSize];

    blobInput.Position = 0;

    using (var img = System.Drawing.Image.FromStream(blobInput)) {
        var widthRatio = (double)size.Width / (double)img.Width;
        var heightRatio = (double)size.Height / (double)img.Height;
        var minAspectRatio = Math.Min(widthRatio, heightRatio);
        if (minAspectRatio > 1) {
            size.Width = img.Width;
            size.Width = img.Height;
        }
        else {
            size.Width = (int)(img.Width * minAspectRatio);
            size.Height = (int)(img.Height * minAspectRatio);
        }

        using (Bitmap bitmap = new Bitmap(img, size)) {
            bitmap.Save(output, img.RawFormat);
            imageFormat = img.RawFormat;
        }
    }

    return imageFormat;
}

#endregion