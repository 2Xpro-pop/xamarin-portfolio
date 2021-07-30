using System;
using System.IO;
using Xamarin.Forms;

namespace PhotoEffect
{
    public partial class MainPage : ContentPage
    {
        public static byte[] Image;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnImageNameTapped(object sender, EventArgs e)
        {
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                Image = new byte[stream.Length];
                await stream.ReadAsync(Image, 0, (int)stream.Length);
                stream.Position = 0;
                image.Source = ImageSource.FromStream(() => stream);
            }
        }
    }
}
