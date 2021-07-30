using System.IO;
using System.Threading.Tasks;

namespace PhotoEffect
{
    public interface ISaveImage
    {
        Task StartSaveImage(byte[] stream);
    }
}
