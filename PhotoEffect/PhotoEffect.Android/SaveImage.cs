using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

[assembly: Dependency(typeof(PhotoEffect.Droid.SaveImage))]
namespace PhotoEffect.Droid
{
    public class SaveImage : ISaveImage
    {
        public async Task StartSaveImage(byte[] stream)
        {
            try
            {
                Java.IO.File storagePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
                string path = Path.Combine(storagePath.ToString(), Guid.NewGuid().ToString("N")+".png");
                path.DebugLog();
                await System.IO.File.WriteAllBytesAsync(path, stream);
                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(path)));
                Android.App.Application.Context.SendBroadcast(mediaScanIntent);
            }
            catch (Exception ex)
            {
                ex.ToString().DebugLog();
            }
        }
    }
}