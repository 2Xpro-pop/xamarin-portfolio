using AprLearn.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AprLearn
{
    public partial class MainPage : ContentPage, IExceptionVisualize
    {
        private bool IsTextFocusedAfterError { get; set; } = false;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new DocumentSerializer(this);
            editor.TextChanged += WriteErrorText;
        }

        public void Visualize(ViewModelExceptionOn exceptionOn)
        {
            if(exceptionOn == ViewModelExceptionOn.DocumentDeserelizing)
            {
                editor.Text = "Invalid text";
                editor.TextColor = Color.Red;
                IsTextFocusedAfterError = true;
            }
        }

        private void WriteErrorText(object sender,TextChangedEventArgs args)
        {
            if (IsTextFocusedAfterError)
            {
                editor.TextColor = Color.Black;
                IsTextFocusedAfterError = false;
            }
            if(args.NewTextValue.Contains(" ") || args.NewTextValue.Contains("\t")) 
                editor.Text = editor.Text.Replace("\t", "    ");
        }
    }
}
