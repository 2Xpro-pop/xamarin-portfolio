using System.IO;
using System.Threading.Tasks;

namespace PhotoEffect
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
