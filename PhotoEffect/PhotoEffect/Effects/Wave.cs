using System;
using System.IO;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PhotoEffect.Effects
{
    public class Wave : Models.IEffectModel
    {
        public string Name { get; set; } = nameof(Wave);

        public Stream MutateImage(byte[] stream)
        {
            using (var sourse = Image.Load<Rgba32>(stream))
            {
                using (var img = new Image<Rgba32>(sourse.Width,sourse.Height))
                {
                    for (int y = 0; y < sourse.Height; ++y)
                    {
                        Span<Rgba32> pixelRowSpan = img.GetPixelRowSpan(y);
                        for (int x = 0; x < sourse.Width; ++x)
                        {

                            int y1 = Convert.ToInt32(y + 20.0 * Math.Sin(x / 32.0));

                            if (y1 >= sourse.Height)
                                y1 = sourse.Height - 1;
                            if (y1 < 0)
                                y1 = 0;

                            var sourcePixel = sourse.GetPixelRowSpan(y1)[x];

                            pixelRowSpan[x] = sourcePixel;
                        }
                    }
                    Stream image = new MemoryStream();
                    img.SaveAsPng(image);
                    image.Position = 0;
                    System.Diagnostics.Debug.WriteLine($"позиция {image.Position}, длинна {image.Length}");
                    return image;
                }
            }
        }
    }
}