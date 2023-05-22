using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MoneyGuru.ViewModels
{
    public class AboutViewModel : ContentPage
    {
        public AboutViewModel()
        {
//            InitializeComponent();
        }

        public ICommand OpenWebCommand { get; }
    }
}