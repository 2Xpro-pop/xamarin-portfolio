using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AprLearn
{
    public partial class App : Application
    {
        public static string LocalPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
