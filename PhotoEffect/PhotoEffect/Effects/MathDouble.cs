using System;
using System.IO;
using System.Numerics;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace PhotoEffect.Effects
{
    public class MathDouble : Models.IEffectModel
    {
        public delegate double Operation(double a);
        public string Name { get; set; }
        private Operation _operation;

        public MathDouble(Operation operation, string name = "math")
        {
            if (operation == null)
                throw new ArgumentNullException();
            _operation = operation;
            Name = name;
        }

        public Stream MutateImage(byte[] stream)
        {
            using(var img = Image.Load<Rgba32>(stream))
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Span<Rgba32> pixelRowSpan = img.GetPixelRowSpan(y);
                    for (int x = 0; x < img.Width; x++)
                    {

                        byte r = pixelRowSpan[x].R; 
                        byte g = pixelRowSpan[x].G;
                        byte b = pixelRowSpan[x].B;

                        r = (byte)Math.Abs(_operation.Invoke(r) * 255);
                        g = (byte)Math.Abs(_operation.Invoke(g) * 255); 
                        b = (byte)Math.Abs(_operation.Invoke(b) * 255);

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
