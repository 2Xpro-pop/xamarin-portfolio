using System;
using System.IO;
using System.Numerics;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace PhotoEffect.Effects
{
    public class Sepia : Models.IEffectModel
    {
        public string Name { get; set; } = nameof(Sepia);

        public Stream MutateImage(byte[] stream)
        {
            using(var img = Image.Load<Rgba32>(stream))
            {
                img.Mutate(proc =>
                {
                    proc.Sepia();
                });
                Stream image = new MemoryStream();
                img.SaveAsPng(image);
                image.Position = 0;
                System.Diagnostics.Debug.WriteLine($"позиция {image.Position}, длинна {image.Length}");
                return image;
            }
        }
    }
}
