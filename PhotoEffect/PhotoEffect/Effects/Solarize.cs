using System;
using System.IO;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PhotoEffect.Effects
{
    public class Solarize : Models.IEffectModel
    {
        public string Name { get; set; } = nameof(Solarize);

        public Stream MutateImage(byte[] stream)
        {
            byte treshhold = 75;
            using (var img = Image.Load<Rgba32>(stream))
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Span<Rgba32> pixelRowSpan = img.GetPixelRowSpan(y);
                    for (int x = 0; x < img.Width; x++)
                    {

                        byte r = pixelRowSpan[x].R;
                        byte g = pixelRowSpan[x].G;
                        byte b = pixelRowSpan[x].B;

                        if (r > treshhold)
                            r = (byte)(255 - r);
                        if (g > treshhold)
                            g = (byte)(255 - r);
                        if (b > treshhold)
                            b = (byte)(255 - r);

                        pixelRowSpan[x] = new Rgba32(r, g, b);
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
