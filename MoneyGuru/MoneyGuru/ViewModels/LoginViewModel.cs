using MoneyGuru.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoneyGuru.ViewModels
{
    public class LoginViewModel : ContentPage
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
