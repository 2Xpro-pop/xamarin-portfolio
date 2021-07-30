using System;
using System.IO;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Essentials;

using PhotoEffect.Effects;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace PhotoEffect.ViewModels
{
    public class Effects : BindableObject
    {
        private byte[] image;

        public static BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(Image), typeof(Effects));
        public static BindableProperty PickerProperty = BindableProperty.Create(nameof(Picker), typeof(Picker), typeof(Effects));

        public IList<Models.IEffectModel> EffectModels { get; set; }
        public ICommand MutateImage { get; set; }
        public ICommand SaveImage { get; set; }

        public Picker Picker { get => (Picker)GetValue(PickerProperty); set => SetValue(PickerProperty, value); }
        public Image Image { get => (Image)GetValue(ImageProperty); set => SetValue(ImageProperty, value); }

        public Effects()
        {
            EffectModels = new List<Models.IEffectModel>();

            EffectModels.Add(new Sepia());
            EffectModels.Add(new BlackAndWhite());
            EffectModels.Add(new Solarize());
            EffectModels.Add(new Wave());
            EffectModels.Add(new Dithering());
            EffectModels.Add(new MathDouble(Math.Cos,"Cosinus"));
            EffectModels.Add(new MathDouble(Math.Sin,"Sinus"));
            EffectModels.Add(new MathDouble(Math.Tan,"Tan"));
            EffectModels.Add(new MathDouble(Math.Exp,"E raised"));
            EffectModels.Add(new MathDouble(Math.Sqrt, "Square Root"));

            MutateImage = new Command(Mutate);
            SaveImage = new Command(Save);
        }

        private void Mutate()
        {
            var item = (Models.IEffectModel)Picker.SelectedItem;
            var stream = item.MutateImage(MainPage.Image);
            image = new byte[stream.Length];

            SetImage(stream);
        }

        void SetImage(Stream stream)
        {
            image = new byte[stream.Length];
            for (long i = 0; i < stream.Length; i++ )
            {
                image[i] = (byte)stream.ReadByte();
            }
            stream.Position = 0;

            Image.Source = ImageSource.FromStream(() => stream);
        }

        private async void Save()
        {
            await DependencyService.Get<ISaveImage>().StartSaveImage(image);
        }

    }
}
