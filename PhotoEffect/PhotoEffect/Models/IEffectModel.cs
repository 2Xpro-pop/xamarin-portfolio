using System.IO;

namespace PhotoEffect.Models
{
    public interface IEffectModel
    {
        string Name { get; set; }

        Stream MutateImage(byte[] stream);
    }
}
